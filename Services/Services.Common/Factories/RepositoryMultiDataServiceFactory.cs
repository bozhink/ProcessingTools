namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public abstract class RepositoryMultiDataServiceFactory<TDbModel, TServiceModel> : RepositoryDataServiceFactoryBase<TDbModel, TServiceModel>
    {
        protected abstract Expression<Func<TDbModel, IEnumerable<TServiceModel>>> MapDbModelToServiceModel { get; }

        protected abstract Expression<Func<TServiceModel, IEnumerable<TDbModel>>> MapServiceModelToDbModel { get; }

        protected abstract Expression<Func<TDbModel, object>> SortExpression { get; }

        public override async Task<IQueryable<TServiceModel>> All(IGenericRepository<TDbModel> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            var result = (await repository.All())
                .SelectMany(this.MapDbModelToServiceModel)
                .ToList()
                .AsQueryable();

            return result;
        }

        public override async Task<IQueryable<TServiceModel>> Query(IGenericRepository<TDbModel> repository, int skip, int take)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            var result = (await repository.Query(x => true, this.SortExpression, skip, take))
                .SelectMany(this.MapDbModelToServiceModel)
                .ToList()
                .AsQueryable();

            return result;
        }

        public override async Task<IQueryable<TServiceModel>> Get(IGenericRepository<TDbModel> repository, params object[] ids)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (ids == null || ids.Length < 1)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var mapping = this.MapDbModelToServiceModel.Compile();

            var result = new ConcurrentQueue<TServiceModel>();

            foreach (var id in ids)
            {
                var entity = await repository.Get(id);
                result.Enqueue(mapping.Invoke(entity).FirstOrDefault());
            }

            return new HashSet<TServiceModel>(result).AsQueryable();
        }

        protected override IEnumerable<TDbModel> MapServiceModelsToEntities(TServiceModel[] models)
        {
            return models.AsQueryable()
                .SelectMany(this.MapServiceModelToDbModel)
                .ToList();
        }
    }
}
