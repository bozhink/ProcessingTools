namespace ProcessingTools.Geo.Services.Data.Abstractions
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts;

    public abstract class AbstractGeoMultiDataService<TRepository, TModel, TFilter> : IMultiDataServiceAsync<TModel, TFilter>
        where TRepository : class, IRepositoryAsync<TModel, TFilter>
        where TModel : class, IIntegerIdentifiable
        where TFilter : IFilter
    {
        private readonly TRepository repository;

        protected AbstractGeoMultiDataService(TRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

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

        public virtual Task<TModel> GetByIdAsync(object id) => this.repository.GetByIdAsync(id: id);

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

        public virtual Task<TModel[]> SelectAsync(TFilter filter) => this.repository.SelectAsync(filter);

        public virtual Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending) => this.repository.SelectAsync(filter, skip, take, sortColumn, sortOrder);

        public virtual Task<long> SelectCountAsync(TFilter filter) => this.repository.SelectCountAsync(filter);

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
