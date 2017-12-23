namespace ProcessingTools.Geo.Services.Data.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Enumerations;

    public abstract class AbstractGeoSynonymisableDataService<TRepository, TModel, TFilter, TSynonym, TSynonymFilter> : IDataServiceAsync<TModel, TFilter>, IGeoSynonymisableDataService<TModel, TSynonym, TSynonymFilter>
        where TRepository : class, IRepositoryAsync<TModel, TFilter>, IGeoSynonymisableRepository<TModel, TSynonym, TSynonymFilter>
        where TModel : class, IIntegerIdentifiable, IGeoSynonymisable<TSynonym>
        where TFilter : IFilter
        where TSynonym : class, IGeoSynonym
        where TSynonymFilter : ISynonymFilter
    {
        private readonly TRepository repository;

        protected AbstractGeoSynonymisableDataService(TRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public virtual async Task<object> AddSynonymsAsync(int modelId, params TSynonym[] synonyms)
        {
            var result = await this.repository.AddSynonymsAsync(modelId, synonyms).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

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

        public virtual Task<TModel> GetByIdAsync(object id) => this.repository.GetByIdAsync(id: id);

        public virtual Task<TSynonym> GetSynonymByIdAsync(int modelId, int id) => this.repository.GetSynonymByIdAsync(modelId, id);

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

        public virtual async Task<object> RemoveSynonymsAsync(int modelId, params int[] synonymIds)
        {
            var result = await this.repository.RemoveSynonymsAsync(modelId, synonymIds).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        public virtual Task<TModel[]> SelectAsync(TFilter filter) => this.repository.SelectAsync(filter);

        public virtual Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder) => this.repository.SelectAsync(filter, skip, take, sortColumn, sortOrder);

        public virtual Task<long> SelectCountAsync(TFilter filter) => this.repository.SelectCountAsync(filter);

        public virtual Task<long> SelectSynonymCountAsync(int modelId, TSynonymFilter filter) => this.repository.SelectSynonymCountAsync(modelId, filter);

        public virtual Task<TSynonym[]> SelectSynonymsAsync(int modelId, TSynonymFilter filter) => this.repository.SelectSynonymsAsync(modelId, filter);

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

        public virtual async Task<object> UpdateSynonymsAsync(int modelId, params TSynonym[] synonyms)
        {
            var result = await this.repository.UpdateSynonymsAsync(modelId, synonyms).ConfigureAwait(false);
            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }
    }
}
