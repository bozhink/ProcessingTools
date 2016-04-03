namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Repositories.Contracts;
    using ProcessingTools.Extensions;

    public abstract class RepositoryDataServiceAbstractFactory<TDbModel, TServiceModel> : IDataService<TServiceModel>, IDisposable
    {
        private readonly IGenericRepository<TDbModel> repository;

        public RepositoryDataServiceAbstractFactory(IGenericRepository<TDbModel> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public virtual async Task<int> Add(params TServiceModel[] models)
        {
            var entities = this.MapServiceModelToDbModel(models);

            foreach (var entity in entities)
            {
                await this.repository.Add(entity);
            }

            return await this.repository.SaveChanges();
        }

        public virtual async Task<IQueryable<TServiceModel>> All()
        {
            return (await this.repository.All())
                .SelectMany(e => this.MapDbModelToServiceModel(e));
        }

        public virtual async Task<int> Delete(params object[] ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            foreach (var id in ids)
            {
                await this.repository.Delete(id);
            }

            return await this.repository.SaveChanges();
        }

        public virtual async Task<int> Delete(params TServiceModel[] models)
        {
            var entities = this.MapServiceModelToDbModel(models);

            foreach (var entity in entities)
            {
                await this.repository.Delete(entity);
            }

            return await this.repository.SaveChanges();
        }

        public virtual async Task<IQueryable<TServiceModel>> Get(params object[] ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var result = new ConcurrentQueue<TServiceModel>();
            foreach (var id in ids)
            {
                var entity = await this.repository.Get(id);
                result.Enqueue(this.MapDbModelToServiceModel(entity).FirstOrDefault());
            }

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

            return (await this.repository.All(skip, take))
                .SelectMany(e => this.MapDbModelToServiceModel(e));
        }

        public virtual async Task<int> Update(params TServiceModel[] models)
        {
            var entities = this.MapServiceModelToDbModel(models);

            foreach (var entity in entities)
            {
                await this.repository.Update(entity);
            }

            return await this.repository.SaveChanges();
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
                this.repository.TryDispose();
            }
        }

        protected abstract IEnumerable<TDbModel> MapServiceModelToDbModel(params TServiceModel[] models);

        protected abstract IEnumerable<TServiceModel> MapDbModelToServiceModel(params TDbModel[] entities);
    }
}