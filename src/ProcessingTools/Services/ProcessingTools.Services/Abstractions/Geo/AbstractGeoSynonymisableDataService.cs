// <copyright file="AbstractGeoSynonymisableDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Abstractions.Geo
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts;
    using ProcessingTools.Services.Contracts.Geo;

    /// <summary>
    /// Abstract geo synonymisable data service.
    /// </summary>
    /// <typeparam name="TRepository">Type of repository.</typeparam>
    /// <typeparam name="TModel">Type of service model.</typeparam>
    /// <typeparam name="TFilter">Type of filter.</typeparam>
    /// <typeparam name="TSynonym">Type of synonym service model.</typeparam>
    /// <typeparam name="TSynonymFilter">Type of synonym filter.</typeparam>
    public abstract class AbstractGeoSynonymisableDataService<TRepository, TModel, TFilter, TSynonym, TSynonymFilter> : IDataServiceAsync<TModel, TFilter>, IGeoSynonymisableDataService<TModel, TSynonym, TSynonymFilter>
        where TRepository : class, IRepositoryAsync<TModel, TFilter>, IGeoSynonymisableRepository<TModel, TSynonym, TSynonymFilter>
        where TModel : class, IIntegerIdentifiable, IGeoSynonymisable<TSynonym>
        where TFilter : IFilter
        where TSynonym : class, IGeoSynonym
        where TSynonymFilter : ISynonymFilter
    {
        private readonly TRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractGeoSynonymisableDataService{TRepository, TModel, TFilter, TSynonym, TSynonymFilter}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        protected AbstractGeoSynonymisableDataService(TRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public virtual async Task<object> AddSynonymsAsync(int modelId, params TSynonym[] synonyms)
        {
            var result = await this.repository.AddSynonymsAsync(modelId, synonyms).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
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
        public virtual Task<TSynonym> GetSynonymByIdAsync(int modelId, int id) => this.repository.GetSynonymByIdAsync(modelId, id);

        /// <inheritdoc/>
        public virtual async Task<object> InsertAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = await this.repository.InsertAsync(model: model).ConfigureAwait(false) as INameableIntegerIdentifiable;
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result?.Id;
        }

        /// <inheritdoc/>
        public virtual async Task<object> InsertAsync(TModel model, params TSynonym[] synonyms)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = await this.repository.InsertAsync(model, synonyms).ConfigureAwait(false) as INameableIntegerIdentifiable;
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result?.Id;
        }

        /// <inheritdoc/>
        public virtual async Task<object> RemoveSynonymsAsync(int modelId, params int[] synonymIds)
        {
            var result = await this.repository.RemoveSynonymsAsync(modelId, synonymIds).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public virtual Task<TModel[]> SelectAsync(TFilter filter) => this.repository.SelectAsync(filter);

        /// <inheritdoc/>
        public virtual Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder) => this.repository.SelectAsync(filter, skip, take, sortColumn, sortOrder);

        /// <inheritdoc/>
        public virtual Task<long> SelectCountAsync(TFilter filter) => this.repository.SelectCountAsync(filter);

        /// <inheritdoc/>
        public virtual Task<long> SelectSynonymCountAsync(int modelId, TSynonymFilter filter) => this.repository.SelectSynonymCountAsync(modelId, filter);

        /// <inheritdoc/>
        public virtual Task<TSynonym[]> SelectSynonymsAsync(int modelId, TSynonymFilter filter) => this.repository.SelectSynonymsAsync(modelId, filter);

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

        /// <inheritdoc/>
        public virtual async Task<object> UpdateSynonymsAsync(int modelId, params TSynonym[] synonyms)
        {
            var result = await this.repository.UpdateSynonymsAsync(modelId, synonyms).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }
    }
}
