// <copyright file="AbstractGeoDataService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Abstractions.Geo
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Data.Contracts;

    /// <summary>
    /// Abstract geo data service.
    /// </summary>
    /// <typeparam name="TRepository">Type of repository.</typeparam>
    /// <typeparam name="TModel">Type of service model.</typeparam>
    /// <typeparam name="TFilter">Type of filter.</typeparam>
    public abstract class AbstractGeoDataService<TRepository, TModel, TFilter> : IDataServiceAsync<TModel, TFilter>
        where TRepository : class, IRepositoryAsync<TModel, TFilter>
        where TModel : class, IIntegerIdentified
        where TFilter : IFilter
    {
        private readonly TRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractGeoDataService{TRepository, TModel, TFilter}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        protected AbstractGeoDataService(TRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public virtual Task<object> DeleteAsync(TModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.DeleteInternalAsync(model);
        }

        /// <inheritdoc/>
        public virtual Task<object> DeleteByIdAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.DeleteByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public virtual Task<TModel> GetByIdAsync(object id) => this.repository.GetByIdAsync(id: id);

        /// <inheritdoc/>
        public virtual Task<object> InsertAsync(TModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public virtual Task<TModel[]> SelectAsync(TFilter filter) => this.repository.SelectAsync(filter);

        /// <inheritdoc/>
        public virtual Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder) => this.repository.SelectAsync(filter, skip, take, sortColumn, sortOrder);

        /// <inheritdoc/>
        public virtual Task<long> SelectCountAsync(TFilter filter) => this.repository.SelectCountAsync(filter);

        /// <inheritdoc/>
        public virtual Task<object> UpdateAsync(TModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.UpdateInternalAsync(model);
        }

        private async Task<object> DeleteByIdInternalAsync(object id)
        {
            var result = await this.repository.DeleteAsync(id: id).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        private async Task<object> DeleteInternalAsync(TModel model)
        {
            var result = await this.repository.DeleteAsync(model: model).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        private async Task<object> InsertInternalAsync(TModel model)
        {
            var result = await this.repository.InsertAsync(model).ConfigureAwait(false) as INamedIntegerIdentified;
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result?.Id;
        }

        private async Task<object> UpdateInternalAsync(TModel model)
        {
            var result = await this.repository.UpdateAsync(model: model).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }
    }
}
