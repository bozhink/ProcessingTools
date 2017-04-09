namespace ProcessingTools.Services.Common.Abstractions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;

    public abstract class AbstractDataService<TDbModel, TServiceModel> : IDataService<TServiceModel>
        where TDbModel : class
        where TServiceModel : class
    {
        public AbstractDataService(ISearchableCountableCrudRepositoryProvider<TDbModel> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.RepositoryProvider = repositoryProvider;

            this.MapDataToServiceModelFunc = this.MapDataToServiceModel.Compile();
            this.MapServiceToDataModelFunc = this.MapServiceToDataModel.Compile();
        }

        protected abstract Expression<Func<TDbModel, TServiceModel>> MapDataToServiceModel { get; }

        protected abstract Expression<Func<TServiceModel, TDbModel>> MapServiceToDataModel { get; }

        protected ISearchableCountableCrudRepositoryProvider<TDbModel> RepositoryProvider { get; private set; }

        private Func<TDbModel, TServiceModel> MapDataToServiceModelFunc { get; set; }

        private Func<TServiceModel, TDbModel> MapServiceToDataModelFunc { get; set; }

        public virtual async Task<object> Add(TServiceModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.MapServiceToDataModelFunc.Invoke(model);

            var repository = this.RepositoryProvider.Create();

            await repository.Add(entity: entity);
            var result = await repository.SaveChangesAsync();

            repository.TryDispose();

            return result;
        }

        public virtual async Task<IQueryable<TServiceModel>> All()
        {
            var repository = this.RepositoryProvider.Create();

            var result = await repository.Query
                .Select(this.MapDataToServiceModel)
                .ToListAsync();

            repository.TryDispose();

            return result.AsQueryable();
        }

        public virtual async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var repository = this.RepositoryProvider.Create();

            await repository.Delete(id: id);
            var result = await repository.SaveChangesAsync();

            repository.TryDispose();

            return result;
        }

        public virtual async Task<TServiceModel> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var repository = this.RepositoryProvider.Create();

            var entity = await repository.GetById(id: id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            var result = this.MapDataToServiceModelFunc.Invoke(entity);

            repository.TryDispose();

            return result;
        }

        public virtual async Task<object> Update(TServiceModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.MapServiceToDataModelFunc.Invoke(model);

            var repository = this.RepositoryProvider.Create();

            await repository.Update(entity: entity);
            var result = await repository.SaveChangesAsync();

            repository.TryDispose();

            return result;
        }
    }
}
