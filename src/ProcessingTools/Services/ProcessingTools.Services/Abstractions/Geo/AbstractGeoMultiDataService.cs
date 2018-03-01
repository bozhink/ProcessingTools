// <copyright file="AbstractGeoMultiDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Abstractions.Geo
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Contracts;

    /// <summary>
    /// Abstract geo multi data service.
    /// </summary>
    /// <typeparam name="TRepository">Type of repository.</typeparam>
    /// <typeparam name="TModel">Type of service model.</typeparam>
    /// <typeparam name="TFilter">Type of filter.</typeparam>
    public abstract class AbstractGeoMultiDataService<TRepository, TModel, TFilter> : IMultiDataServiceAsync<TModel, TFilter>
        where TRepository : class, IRepositoryAsync<TModel, TFilter>
        where TModel : class, IIntegerIdentifiable
        where TFilter : IFilter
    {
        private readonly TRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractGeoMultiDataService{TRepository, TModel, TFilter}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        protected AbstractGeoMultiDataService(TRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public virtual async Task<object> DeleteAsync(params TModel[] models)
        {
            if (models == null || models.Length < 1)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var queue = new ConcurrentQueue<object>();

            var tasks = models.Select(
                async model =>
                {
                    var result = await this.repository.DeleteAsync(model: model).ConfigureAwait(false);
                    queue.Enqueue(result);
                })
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return queue.ToArray<object>();
        }

        /// <inheritdoc/>
        public virtual async Task<object> DeleteAsync(params object[] ids)
        {
            if (ids == null || ids.Length < 1)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var queue = new ConcurrentQueue<object>();

            var tasks = ids.Select(
                async id =>
                {
                    var result = await this.repository.DeleteAsync(id: id).ConfigureAwait(false);
                    queue.Enqueue(result);
                })
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return queue.ToArray<object>();
        }

        /// <inheritdoc/>
        public virtual Task<TModel> GetByIdAsync(object id) => this.repository.GetByIdAsync(id: id);

        /// <inheritdoc/>
        public virtual async Task<object> InsertAsync(params TModel[] models)
        {
            if (models == null || models.Length < 1)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var queue = new ConcurrentQueue<object>();

            var tasks = models.Select(
                async model =>
                {
                    var result = await this.repository.InsertAsync(model).ConfigureAwait(false);
                    queue.Enqueue(result);
                })
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return queue.Select(r => (r as INameableIntegerIdentifiable)?.Id).ToArray();
        }

        /// <inheritdoc/>
        public virtual Task<TModel[]> SelectAsync(TFilter filter) => this.repository.SelectAsync(filter);

        /// <inheritdoc/>
        public virtual Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder) => this.repository.SelectAsync(filter, skip, take, sortColumn, sortOrder);

        /// <inheritdoc/>
        public virtual Task<long> SelectCountAsync(TFilter filter) => this.repository.SelectCountAsync(filter);

        /// <inheritdoc/>
        public virtual async Task<object> UpdateAsync(params TModel[] models)
        {
            if (models == null || models.Length < 1)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var queue = new ConcurrentQueue<object>();

            var tasks = models.Select(
                async model =>
                {
                    var result = await this.repository.UpdateAsync(model).ConfigureAwait(false);
                    queue.Enqueue(result);
                })
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return queue.Select(r => (r as INameableIntegerIdentifiable)?.Id).ToArray();
        }
    }
}
