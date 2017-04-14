namespace ProcessingTools.Services.Common.Abstractions
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;

    public abstract class AbstractDataServiceWithRepository<TDbModel, TServiceModel> : IDisposable
    {
        private readonly ICrudRepository<TDbModel> repository;

        public AbstractDataServiceWithRepository(ICrudRepository<TDbModel> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        protected abstract Expression<Func<TDbModel, TServiceModel>> MapDbModelToServiceModel { get; }

        protected abstract Expression<Func<TServiceModel, TDbModel>> MapServiceModelToDbModel { get; }

        protected abstract Expression<Func<TDbModel, object>> SortExpression { get; }

        public virtual async Task<object> Add(params TServiceModel[] models)
        {
            if (models == null || models.Length < 1)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = this.MapServiceModelsToEntities(models);

            var tasks = entities.Select(e => this.repository.Add(e)).ToArray();

            await Task.WhenAll(tasks);

            return await this.repository.SaveChangesAsync();
        }

        public virtual async Task<IQueryable<TServiceModel>> All()
        {
            var result = await this.repository.Query
                .Select(this.MapDbModelToServiceModel)
                .ToListAsync();

            return result.AsQueryable();
        }

        public virtual async Task<IQueryable<TServiceModel>> Query(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            var result = await this.repository.Query
                .OrderBy(this.SortExpression)
                .Skip(skip)
                .Take(take)
                .Select(this.MapDbModelToServiceModel)
                .ToListAsync();

            return result.AsQueryable();
        }

        public virtual async Task<object> Delete(params object[] ids)
        {
            if (ids == null || ids.Length < 1)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var tasks = ids.Select(id => this.repository.Delete(id)).ToArray();

            await Task.WhenAll(tasks);

            return await this.repository.SaveChangesAsync();
        }

        public virtual async Task<object> Delete(params TServiceModel[] models)
        {
            if (models == null || models.Length < 1)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = this.MapServiceModelsToEntities(models);

            var tasks = entities.Select(e => this.repository.Delete(e)).ToArray();

            await Task.WhenAll(tasks);

            return await this.repository.SaveChangesAsync();
        }

        public virtual async Task<IQueryable<TServiceModel>> Get(params object[] ids)
        {
            if (ids == null || ids.Length < 1)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var mapping = this.MapDbModelToServiceModel.Compile();

            var result = new ConcurrentQueue<TServiceModel>();

            foreach (var id in ids)
            {
                var entity = await this.repository.GetById(id);
                result.Enqueue(mapping.Invoke(entity));
            }

            return new HashSet<TServiceModel>(result).AsQueryable();
        }

        public virtual async Task<object> Update(params TServiceModel[] models)
        {
            if (models == null || models.Length < 1)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = this.MapServiceModelsToEntities(models);

            var tasks = entities.Select(e => this.repository.Update(e)).ToArray();

            await Task.WhenAll(tasks);

            return await this.repository.SaveChangesAsync();
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

        private IEnumerable<TDbModel> MapServiceModelsToEntities(TServiceModel[] models)
        {
            return models.AsQueryable()
                .Select(this.MapServiceModelToDbModel)
                .ToList();
        }
    }
}
