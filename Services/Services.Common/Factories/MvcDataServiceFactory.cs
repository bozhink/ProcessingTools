namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;

    public abstract class MvcDataServiceFactory<TMinimalServiceModel, TServiceModel, TDetailsServiceModel, TDbModel> : IMvcDataService<TMinimalServiceModel, TServiceModel, TDetailsServiceModel>
        where TMinimalServiceModel : class
        where TServiceModel : class
        where TDetailsServiceModel : class
        where TDbModel : class, IModelWithUserInformation
    {
        private readonly ISearchableCountableCrudRepositoryProvider<TDbModel> repositoryProvider;

        public MvcDataServiceFactory(ISearchableCountableCrudRepositoryProvider<TDbModel> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        protected abstract Expression<Func<TDbModel, TServiceModel>> MapDbModelToServiceModel { get; }

        protected ISearchableCountableCrudRepositoryProvider<TDbModel> RepositoryProvider => this.repositoryProvider;

        public abstract Task<object> Add(object userId, TMinimalServiceModel model);

        public virtual async Task<IQueryable<TServiceModel>> All(int pageNumber, int itemsPerPage)
        {
            if (pageNumber < 0)
            {
                throw new InvalidPageNumberException();
            }

            if (1 > itemsPerPage || itemsPerPage > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var repository = this.RepositoryProvider.Create();

            var models = await repository.Query
                .OrderByDescending(d => d.DateModified)
                .Skip(pageNumber * itemsPerPage)
                .Take(itemsPerPage)
                .Select(this.MapDbModelToServiceModel)
                .ToListAsync();

            repository.TryDispose();

            return models.AsQueryable();
        }

        public virtual async Task<long> Count()
        {
            var repository = this.RepositoryProvider.Create();

            long count = await repository.Count();

            repository.TryDispose();

            return count;
        }

        public virtual async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var repository = this.RepositoryProvider.Create();

            await repository.Delete(id);
            var result = await repository.SaveChanges();

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

            var entity = await repository.Get(id);
            if (entity == null)
            {
                repository.TryDispose();
                throw new EntityNotFoundException();
            }

            var result = this.MapDbModelToServiceModel.Compile().Invoke(entity);

            repository.TryDispose();

            return result;
        }

        public abstract Task<TDetailsServiceModel> GetDetails(object id);

        public abstract Task<object> Update(object userId, TMinimalServiceModel model);
    }
}
