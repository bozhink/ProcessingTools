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
    using ProcessingTools.Data.Common.Repositories.Contracts;
    using ProcessingTools.Extensions;

    public abstract class GenericRepositoryDataServiceFactory<TDbModel, TServiceModel> : IDataService<TServiceModel>, IDisposable
    {
        private readonly IGenericRepository<TDbModel> repository;

        public GenericRepositoryDataServiceFactory(IGenericRepository<TDbModel> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        protected abstract Expression<Func<TServiceModel, IEnumerable<TDbModel>>> MapServiceModelToDbModel { get; }

        protected abstract Expression<Func<TDbModel, IEnumerable<TServiceModel>>> MapDbModelToServiceModel { get; }

        public virtual async Task<int> Add(params TServiceModel[] models)
        {
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = models.AsQueryable()
                .SelectMany(this.MapServiceModelToDbModel)
                .ToList();

            foreach (var entity in entities)
            {
                await this.repository.Add(entity);
            }

            return await this.repository.SaveChanges();
        }

        public virtual async Task<IQueryable<TServiceModel>> All()
        {
            return (await this.repository.All())
                .SelectMany(this.MapDbModelToServiceModel);
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
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = models.AsQueryable()
                .SelectMany(this.MapServiceModelToDbModel)
                .ToList();

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

            var mapping = this.MapDbModelToServiceModel.Compile();

            var result = new ConcurrentQueue<TServiceModel>();
            foreach (var id in ids)
            {
                var entity = await this.repository.Get(id);
                result.Enqueue(mapping.Invoke(entity).FirstOrDefault());
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
                .SelectMany(this.MapDbModelToServiceModel);
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
    }
}