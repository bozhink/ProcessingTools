namespace ProcessingTools.Geo.Data.Entity.Abstractions.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;

    public abstract class AbstractGeoRepository<TEntity, TModel, TFilter> : IRepositoryAsync<TModel, TFilter>
        where TEntity : SystemInformation, IDataModel
        where TModel : class, IIntegerIdentifiable
        where TFilter : IFilter
    {
        private readonly IGeoRepository<TEntity> repository;
        private readonly IEnvironment environment;

        protected AbstractGeoRepository(IGeoRepository<TEntity> repository, IEnvironment environment)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        protected IGeoRepository<TEntity> Repository => this.repository;

        protected IEnvironment Environment => this.environment;

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

        public virtual async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.repository.Delete(id: id);
            return await Task.FromResult(id);
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
            return await this.InsertEntityAsync(entity);
        }

        public virtual Task<object> SaveChangesAsync() => this.repository.SaveChangesAsync();

        public virtual async Task<TModel[]> SelectAsync(TFilter filter)
        {
            var query = this.GetQuery(filter);

            var data = await query.ToList()
                .Select(this.MapEntityToModel)
                .ToArrayAsync();

            return data;
        }

        public virtual async Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending)
        {
            var query = this.GetQuery(filter)
                .OrderByName(sortColumn, sortOrder)
                .Skip(skip)
                .Take(take);

            var data = await query.ToList()
                .Select(this.MapEntityToModel)
                .ToArrayAsync();

            return data;
        }

        public virtual async Task<long> SelectCountAsync(TFilter filter)
        {
            var query = this.GetQuery(filter);
            var count = await query.LongCountAsync();
            return count;
        }

        public virtual async Task<object> UpdateAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.MapModelToEntity(model);
            return await this.UpdateEntityAsync(entity);
        }

        protected async Task<TEntity> InsertEntityAsync(TEntity entity)
        {
            string user = this.environment.User.Id;
            var now = this.environment.DateTime.Now;

            entity.CreatedBy = user;
            entity.CreatedOn = now;
            entity.ModifiedBy = user;
            entity.ModifiedOn = now;

            this.repository.Add(entity);
            return await Task.FromResult(entity);
        }

        protected async Task<TEntity> UpdateEntityAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            string user = this.environment.User.Id;
            var now = this.environment.DateTime.Now;

            entity.ModifiedBy = user;
            entity.ModifiedOn = now;

            this.repository.Update(entity);
            return await Task.FromResult(entity);
        }

        protected abstract IQueryable<TEntity> GetQuery(TFilter filter);
    }
}
