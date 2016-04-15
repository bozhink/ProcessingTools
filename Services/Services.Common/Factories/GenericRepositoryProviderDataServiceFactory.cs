namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;
    using ProcessingTools.Extensions;

    public abstract class GenericRepositoryProviderDataServiceFactory<TDbModel, TServiceModel> : IDataService<TServiceModel>, IDisposable
    {
        private readonly IGenericRepositoryProvider<IGenericRepository<TDbModel>, TDbModel> repositoryProvider;

        public GenericRepositoryProviderDataServiceFactory(IGenericRepositoryProvider<IGenericRepository<TDbModel>, TDbModel> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        protected abstract Expression<Func<TServiceModel, IEnumerable<TDbModel>>> MapServiceModelToDbModel { get; }

        protected abstract Expression<Func<TDbModel, IEnumerable<TServiceModel>>> MapDbModelToServiceModel { get; }

        protected abstract Expression<Func<TDbModel, object>> SortExpression { get; }

        public virtual async Task<int> Add(params TServiceModel[] models)
        {
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = models.AsQueryable()
                .SelectMany(this.MapServiceModelToDbModel)
                .ToList();

            var repository = this.repositoryProvider.Create();
            foreach (var entity in entities)
            {
                await repository.Add(entity);
            }

            int savedItems = await repository.SaveChanges();
            repository.TryDispose();
            return savedItems;
        }

        public virtual async Task<IQueryable<TServiceModel>> All()
        {
            var repository = this.repositoryProvider.Create();
            var result = (await repository.All())
                .SelectMany(this.MapDbModelToServiceModel)
                .ToList()
                .AsQueryable();
            repository.TryDispose();
            return result;
        }

        public virtual async Task<int> Delete(params object[] ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var repository = this.repositoryProvider.Create();
            foreach (var id in ids)
            {
                await repository.Delete(id);
            }

            int savedItems = await repository.SaveChanges();
            repository.TryDispose();
            return savedItems;
        }

        public virtual async Task<int> Delete(params TServiceModel[] models)
        {
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = models.AsQueryable()
                .SelectMany(this.MapServiceModelToDbModel)
                .ToList();

            var repository = this.repositoryProvider.Create();
            foreach (var entity in entities)
            {
                await repository.Delete(entity);
            }

            int savedItems = await repository.SaveChanges();
            repository.TryDispose();
            return savedItems;
        }

        public virtual async Task<IQueryable<TServiceModel>> Get(params object[] ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var mapping = this.MapDbModelToServiceModel.Compile();

            var result = new ConcurrentQueue<TServiceModel>();

            var repository = this.repositoryProvider.Create();
            foreach (var id in ids)
            {
                var entity = await repository.Get(id);
                result.Enqueue(mapping.Invoke(entity).FirstOrDefault());
            }

            repository.TryDispose();

            return new HashSet<TServiceModel>(result).AsQueryable();
        }

        public virtual async Task<IQueryable<TServiceModel>> Get(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            var repository = this.repositoryProvider.Create();
            var result = (await repository.All(this.SortExpression, skip, take))
                .SelectMany(this.MapDbModelToServiceModel)
                .ToList()
                .AsQueryable();
            repository.TryDispose();
            return result;
        }

        public virtual async Task<int> Update(params TServiceModel[] models)
        {
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = models.AsQueryable()
                .SelectMany(this.MapServiceModelToDbModel)
                .ToList();

            var repository = this.repositoryProvider.Create();
            foreach (var entity in entities)
            {
                await repository.Update(entity);
            }

            int savedItems = await repository.SaveChanges();
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
