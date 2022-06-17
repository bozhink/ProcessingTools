// <copyright file="AbstractGeoRepository.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Geo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Models.Entity.Geo;
    using ProcessingTools.Extensions.Linq;

    public abstract class AbstractGeoRepository<TEntity, TModel, TFilter> : IRepositoryAsync<TModel, TFilter>
        where TEntity : BaseModel
        where TModel : class, IIntegerIdentified
        where TFilter : IFilter
    {
        protected AbstractGeoRepository(IGeoRepository<TEntity> repository, IApplicationContext applicationContext)
        {
            this.Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.ApplicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        protected IGeoRepository<TEntity> Repository { get; }

        protected IApplicationContext ApplicationContext { get; }

        protected abstract Func<TEntity, TModel> MapEntityToModel { get; }

        protected abstract Func<TModel, TEntity> MapModelToEntity { get; }

        /// <inheritdoc/>
        public virtual Task<object> DeleteAsync(TModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            object id = model.Id;
            return this.DeleteAsync(id: id);
        }

        /// <inheritdoc/>
        public virtual Task<object> DeleteAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Repository.Delete(id: id);
            return Task.FromResult(id);
        }

        /// <inheritdoc/>
        public virtual Task<TModel> GetByIdAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = this.Repository.Get(id);
            if (entity is null)
            {
                return Task.FromResult<TModel>(null);
            }

            var model = this.MapEntityToModel(entity);
            return Task.FromResult(model);
        }

        /// <inheritdoc/>
        public virtual async Task<object> InsertAsync(TModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.MapModelToEntity(model);
            return await this.InsertEntityAsync(entity).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<object> SaveChangesAsync() => this.Repository.SaveChangesAsync();

        /// <inheritdoc/>
        public virtual async Task<TModel[]> SelectAsync(TFilter filter)
        {
            var query = this.GetQuery(filter);

            var data = query.ToList();
            var result = await data.Select(this.MapEntityToModel).ToArrayAsync().ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, ProcessingTools.Common.Enumerations.SortOrder sortOrder)
        {
            var query = this.GetQuery(filter)
                .OrderByName(sortColumn, (ProcessingTools.Extensions.Linq.SortOrder)sortOrder)
                .Skip(skip)
                .Take(take);

            var data = query.ToList();
            var result = await data.Select(this.MapEntityToModel).ToArrayAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<long> SelectCountAsync(TFilter filter)
        {
            var query = this.GetQuery(filter);
            var count = await query.LongCountAsync().ConfigureAwait(false);
            return count;
        }

        /// <inheritdoc/>
        public virtual async Task<object> UpdateAsync(TModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.MapModelToEntity(model);
            return await this.UpdateEntityAsync(entity).ConfigureAwait(false);
        }

        protected async Task<TEntity> InsertEntityAsync(TEntity entity)
        {
            string? user = this.ApplicationContext.UserContext?.UserId;
            var now = this.ApplicationContext.DateTimeProvider.Invoke();

            entity.CreatedBy = user;
            entity.CreatedOn = now;
            entity.ModifiedBy = user;
            entity.ModifiedOn = now;

            this.Repository.Add(entity);
            return await Task.FromResult(entity).ConfigureAwait(false);
        }

        protected async Task<TEntity> UpdateEntityAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            string user = this.ApplicationContext.UserContext?.UserId;
            var now = this.ApplicationContext.DateTimeProvider.Invoke();

            entity.ModifiedBy = user;
            entity.ModifiedOn = now;

            this.Repository.Update(entity);
            return await Task.FromResult(entity).ConfigureAwait(false);
        }

        protected abstract IQueryable<TEntity> GetQuery(TFilter filter);
    }
}
