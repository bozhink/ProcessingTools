﻿namespace ProcessingTools.Data.Entity.Abstractions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Common.Code.Data.Expressions;
    using ProcessingTools.Contracts.Data.Expressions;

    public abstract class EntityRepository<TContext, TDbModel, TEntity> : EntityRepository<TContext, TDbModel>, IEntitySearchableRepository<TEntity>, IEntityCrudRepository<TEntity>
        where TContext : DbContext
        where TEntity : class
        where TDbModel : class, TEntity
    {
        protected EntityRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }

        public virtual IQueryable<TEntity> Query => this.DbSet.AsQueryable<TEntity>();

        protected abstract Func<TEntity, TDbModel> MapEntityToDbModel { get; }

        public virtual async Task<object> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbmodel = this.MapEntityToDbModel.Invoke(entity);
            return await this.AddAsync(dbmodel, this.DbSet).ConfigureAwait(false);
        }

        public virtual Task<long> CountAsync()
        {
            return this.DbSet.LongCountAsync();
        }

        public virtual Task<long> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return this.DbSet.Where(filter).LongCountAsync();
        }

        public virtual async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await this.GetAsync(id, this.DbSet).ConfigureAwait(false);
            if (entity == null)
            {
                return null;
            }

            var entry = this.GetEntry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
                return entity;
            }
            else
            {
                this.DbSet.Attach(entity);
                return this.DbSet.Remove(entity);
            }
        }

        // TODO
        public virtual async Task<TEntity[]> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.DbSet.Where(filter);
            var data = await query.ToArrayAsync().ConfigureAwait(false);
            return data;
        }

        public virtual Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return this.DbSet.FirstOrDefaultAsync(filter);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id) => await this.GetAsync(id, this.DbSet).ConfigureAwait(false);

        public virtual async Task<object> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbmodel = this.MapEntityToDbModel.Invoke(entity);
            return await this.UpdateAsync(dbmodel, this.DbSet).ConfigureAwait(false);
        }

        protected Task<T> AddAsync<T>(T entity, DbSet<T> set)
            where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            var entry = this.GetEntry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
                return Task.FromResult(entity);
            }
            else
            {
                set.Add(entity);
                return Task.FromResult(entity);
            }
        }

        protected virtual Task<T> GetAsync<T>(object id, DbSet<T> set)
            where T : class
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return Task.Run(() => set.Find(id));
        }

        protected Task<T> UpdateAsync<T>(T entity, DbSet<T> set)
            where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            var entry = this.GetEntry(entity);
            if (entry.State == EntityState.Detached)
            {
                set.Attach(entity);
            }

            entry.State = EntityState.Modified;
            return Task.FromResult(entity);
        }
    }
}
