// <copyright file="AbstractGeoDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Abstractions.Geo
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Contracts;

    /// <summary>
    /// Abstract geo data service.
    /// </summary>
    /// <typeparam name="TRepository">Type of repository.</typeparam>
    /// <typeparam name="TModel">Type of service model.</typeparam>
    /// <typeparam name="TFilter">Type of filter.</typeparam>
    public abstract class AbstractGeoDataService<TRepository, TModel, TFilter> : IDataServiceAsync<TModel, TFilter>
        where TRepository : class, IRepositoryAsync<TModel, TFilter>
        where TModel : class, IIntegerIdentifiable
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
        public virtual async Task<object> DeleteAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = await this.repository.DeleteAsync(model: model).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<object> DeleteByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await this.repository.DeleteAsync(id: id).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public virtual Task<TModel> GetByIdAsync(object id) => this.repository.GetByIdAsync(id: id);

        /// <inheritdoc/>
        public virtual async Task<object> InsertAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = await this.repository.InsertAsync(model).ConfigureAwait(false) as INameableIntegerIdentifiable;
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result?.Id;
        }

        /// <inheritdoc/>
        public virtual Task<TModel[]> SelectAsync(TFilter filter) => this.repository.SelectAsync(filter);

        /// <inheritdoc/>
        public virtual Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder) => this.repository.SelectAsync(filter, skip, take, sortColumn, sortOrder);

        /// <inheritdoc/>
        public virtual Task<long> SelectCountAsync(TFilter filter) => this.repository.SelectCountAsync(filter);

        /// <inheritdoc/>
        public virtual async Task<object> UpdateAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = await this.repository.UpdateAsync(model: model).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }
    }
}
