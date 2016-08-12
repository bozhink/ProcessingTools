namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Common.Repositories.Contracts;
    using ProcessingTools.Extensions;

    public abstract class SimpleDataServiceWithRepositoryProviderFactory<TDbModel, TServiceModel> : RepositoryDataServiceFactory<TDbModel, TServiceModel>, IMultiEntryDataService<TServiceModel>, IDisposable
    {
        private readonly IGenericRepositoryProvider<TDbModel> repositoryProvider;

        public SimpleDataServiceWithRepositoryProviderFactory(IGenericRepositoryProvider<TDbModel> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        protected override abstract Expression<Func<TDbModel, TServiceModel>> MapDbModelToServiceModel { get; }

        protected override abstract Expression<Func<TServiceModel, TDbModel>> MapServiceModelToDbModel { get; }

        protected override abstract Expression<Func<TDbModel, object>> SortExpression { get; }

        public virtual async Task<int> Add(params TServiceModel[] models)
        {
            var repository = this.repositoryProvider.Create();

            int savedItems = await base.Add(repository: repository, models: models);

            repository.TryDispose();

            return savedItems;
        }

        public virtual async Task<IQueryable<TServiceModel>> All()
        {
            var repository = this.repositoryProvider.Create();

            var result = await base.All(repository: repository);

            repository.TryDispose();

            return result;
        }

        public virtual async Task<IQueryable<TServiceModel>> Query(int skip, int take)
        {
            var repository = this.repositoryProvider.Create();

            var result = await base.Query(repository: repository, skip: skip, take: take);

            repository.TryDispose();

            return result;
        }

        public virtual async Task<int> Delete(params object[] ids)
        {
            var repository = this.repositoryProvider.Create();

            int savedItems = await base.Delete(repository: repository, ids: ids);

            repository.TryDispose();

            return savedItems;
        }

        public virtual async Task<int> Delete(params TServiceModel[] models)
        {
            var repository = this.repositoryProvider.Create();

            int savedItems = await base.Delete(repository: repository, models: models);

            repository.TryDispose();

            return savedItems;
        }

        public virtual async Task<IQueryable<TServiceModel>> Get(params object[] ids)
        {
            var repository = this.repositoryProvider.Create();

            var result = await base.Get(repository: repository, ids: ids);

            repository.TryDispose();

            return result;
        }

        public virtual async Task<int> Update(params TServiceModel[] models)
        {
            var repository = this.repositoryProvider.Create();

            int savedItems = await base.Update(repository: repository, models: models);

            repository.TryDispose();

            return savedItems;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.repositoryProvider.TryDispose();
            }
        }
    }
}
