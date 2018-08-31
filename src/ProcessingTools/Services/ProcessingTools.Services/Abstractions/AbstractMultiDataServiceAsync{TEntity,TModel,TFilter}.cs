// <copyright file="AbstractMultiDataServiceAsync{TEntity,TModel,TFilter}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Contracts;

    /// <summary>
    /// Abstract multi data service.
    /// </summary>
    /// <typeparam name="TEntity">Type of DB model.</typeparam>
    /// <typeparam name="TModel">Type of service model.</typeparam>
    /// <typeparam name="TFilter">Type of filter.</typeparam>
    public abstract class AbstractMultiDataServiceAsync<TEntity, TModel, TFilter> : IMultiDataServiceAsync<TModel, TFilter>, IDisposable
        where TFilter : IFilter
    {
        private readonly ICrudRepository<TEntity> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractMultiDataServiceAsync{TEntity, TModel, TFilter}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        protected AbstractMultiDataServiceAsync(ICrudRepository<TEntity> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="AbstractMultiDataServiceAsync{TEntity, TModel, TFilter}"/> class.
        /// </summary>
        ~AbstractMultiDataServiceAsync()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the mapping of DB model to service model.
        /// </summary>
        protected abstract Expression<Func<TEntity, TModel>> MapEntityToModel { get; }

        /// <summary>
        /// Gets the mapping of service model to DB model.
        /// </summary>
        protected abstract Expression<Func<TModel, TEntity>> MapModelToEntity { get; }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public virtual Task<TModel[]> SelectAsync(TFilter filter)
        {
            // TODO: filter
            return Task.Run(() => this.repository.Query.Select(this.MapEntityToModel).ToArray());
        }

        /// <inheritdoc/>
        public virtual Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder)
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

            return Task.Run(() => this.repository.Query.OrderByName(sortColumn, sortOrder).Skip(skip).Take(take).Select(this.MapEntityToModel).ToArray());
        }

        /// <inheritdoc/>
        public virtual Task<long> SelectCountAsync(TFilter filter)
        {
            // TODO: filter
            return Task.Run(() => this.repository.Query.LongCount());
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose implementation.
        /// </summary>
        /// <param name="disposing">Disposing parameter.</param>
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
