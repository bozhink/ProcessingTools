namespace ProcessingTools.Services.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Exceptions;

    public abstract class AbstractMultiDataServiceAsync<TEntity, TModel, TFilter> : IMultiDataServiceAsync<TModel, TFilter>, IDisposable
        where TFilter : IFilter
    {
        private readonly ICrudRepository<TEntity> repository;

        protected AbstractMultiDataServiceAsync(ICrudRepository<TEntity> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        ~AbstractMultiDataServiceAsync()
        {
            this.Dispose(false);
        }

        protected abstract Expression<Func<TEntity, TModel>> MapEntityToModel { get; }

        protected abstract Expression<Func<TModel, TEntity>> MapModelToEntity { get; }

        public virtual async Task<object> DeleteAsync(params object[] ids)
        {
            if (ids == null || ids.Length < 1)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var tasks = ids.Select(id => this.repository.DeleteAsync(id)).ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return await this.repository.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<object> DeleteAsync(params TModel[] models)
        {
            if (models == null || models.Length < 1)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = this.MapModelsToEntities(models);

            var tasks = entities.Select(e => this.repository.DeleteAsync(e)).ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return await this.repository.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<TModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var mapping = this.MapEntityToModel.Compile();

            var entity = await this.repository.GetByIdAsync(id).ConfigureAwait(false);
            var result = mapping.Invoke(entity);

            return result;
        }

        public virtual async Task<object> InsertAsync(params TModel[] models)
        {
            if (models == null || models.Length < 1)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = this.MapModelsToEntities(models);

            var tasks = entities.Select(e => this.repository.AddAsync(e)).ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return await this.repository.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<TModel[]> SelectAsync(TFilter filter)
        {
            // TODO: filter
            var result = await this.repository.Query
                .Select(this.MapEntityToModel)
                .ToArrayAsync()
                .ConfigureAwait(false);

            return result;
        }

        public virtual async Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending)
        {
            // TODO: filter
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (take < 1 || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            var result = await this.repository.Query
                .OrderByName(sortColumn, sortOrder)
                .Skip(skip)
                .Take(take)
                .Select(this.MapEntityToModel)
                .ToArrayAsync()
                .ConfigureAwait(false);

            return result;
        }

        public virtual async Task<long> SelectCountAsync(TFilter filter)
        {
            // TODO: filter
            var count = await this.repository.Query.LongCountAsync().ConfigureAwait(false);

            return count;
        }

        public virtual async Task<object> UpdateAsync(params TModel[] models)
        {
            if (models == null || models.Length < 1)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = this.MapModelsToEntities(models);

            var tasks = entities.Select(e => this.repository.UpdateAsync(e)).ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return await this.repository.SaveChangesAsync().ConfigureAwait(false);
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

        private IEnumerable<TEntity> MapModelsToEntities(TModel[] models)
        {
            return models.AsQueryable()
                .Select(this.MapModelToEntity)
                .ToList();
        }
    }
}
