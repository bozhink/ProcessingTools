namespace ProcessingTools.Geo.Services.Data.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts;

    public abstract class AbstractGeoDataService<TRepository, TModel, TFilter> : IDataServiceAsync<TModel, TFilter>
        where TRepository : class, IRepositoryAsync<TModel, TFilter>
        where TModel : class, IIntegerIdentifiable
        where TFilter : IFilter
    {
        private readonly TRepository repository;

        protected AbstractGeoDataService(TRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
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

        public virtual async Task<object> DeleteAsync(object id)
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

        public virtual Task<TModel[]> SelectAsync(TFilter filter) => this.repository.SelectAsync(filter);

        public virtual Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending) => this.repository.SelectAsync(filter, skip, take, sortColumn, sortOrder);

        public virtual Task<long> SelectCountAsync(TFilter filter) => this.repository.SelectCountAsync(filter);

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
