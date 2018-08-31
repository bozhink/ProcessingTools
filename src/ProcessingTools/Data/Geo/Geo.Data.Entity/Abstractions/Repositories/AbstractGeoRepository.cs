namespace ProcessingTools.Geo.Data.Entity.Abstractions.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Models.Contracts;

    public abstract class AbstractGeoRepository<TEntity, TModel, TFilter> : IRepositoryAsync<TModel, TFilter>
        where TEntity : BaseModel, IDataModel
        where TModel : class, IIntegerIdentifiable
        where TFilter : IFilter
    {
        private readonly IGeoRepository<TEntity> repository;
        private readonly IApplicationContext applicationContext;

        protected AbstractGeoRepository(IGeoRepository<TEntity> repository, IApplicationContext applicationContext)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        protected IGeoRepository<TEntity> Repository => this.repository;

        protected IApplicationContext ApplicationContext => this.applicationContext;

        protected abstract Func<TEntity, TModel> MapEntityToModel { get; }

        protected abstract Func<TModel, TEntity> MapModelToEntity { get; }

        public virtual Task<object> DeleteAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            object id = model.Id;
            return this.DeleteAsync(id: id);
        }

        public virtual Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.repository.Delete(id: id);
            return Task.FromResult(id);
        }

        public virtual Task<TModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = this.repository.Get(id);
            if (entity == null)
            {
                return Task.FromResult<TModel>(null);
            }

            var model = this.MapEntityToModel(entity);
            return Task.FromResult(model);
        }

        public virtual async Task<object> InsertAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.MapModelToEntity(model);
            return await this.InsertEntityAsync(entity).ConfigureAwait(false);
        }

        public virtual Task<object> SaveChangesAsync() => this.repository.SaveChangesAsync();

        public virtual async Task<TModel[]> SelectAsync(TFilter filter)
        {
            var query = this.GetQuery(filter);

            var data = query.ToList();
            var result = await data.Select(this.MapEntityToModel).ToArrayAsync().ConfigureAwait(false);
            return result;
        }

        public virtual async Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder)
        {
            var query = this.GetQuery(filter)
                .OrderByName(sortColumn, sortOrder)
                .Skip(skip)
                .Take(take);

            var data = query.ToList();
            var result = await data.Select(this.MapEntityToModel).ToArrayAsync().ConfigureAwait(false);

            return result;
        }

        public virtual async Task<long> SelectCountAsync(TFilter filter)
        {
            var query = this.GetQuery(filter);
            var count = await query.LongCountAsync().ConfigureAwait(false);
            return count;
        }

        public virtual async Task<object> UpdateAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.MapModelToEntity(model);
            return await this.UpdateEntityAsync(entity).ConfigureAwait(false);
        }

        protected async Task<TEntity> InsertEntityAsync(TEntity entity)
        {
            string user = this.applicationContext.UserContext?.UserId;
            var now = this.applicationContext.DateTimeProvider.Invoke();

            entity.CreatedBy = user;
            entity.CreatedOn = now;
            entity.ModifiedBy = user;
            entity.ModifiedOn = now;

            this.repository.Add(entity);
            return await Task.FromResult(entity).ConfigureAwait(false);
        }

        protected async Task<TEntity> UpdateEntityAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            string user = this.applicationContext.UserContext?.UserId;
            var now = this.applicationContext.DateTimeProvider.Invoke();

            entity.ModifiedBy = user;
            entity.ModifiedOn = now;

            this.repository.Update(entity);
            return await Task.FromResult(entity).ConfigureAwait(false);
        }

        protected abstract IQueryable<TEntity> GetQuery(TFilter filter);
    }
}
