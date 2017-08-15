namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Expressions;
    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;
    using ProcessingTools.Data.Common.Expressions;

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

        public virtual async Task<object> Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbmodel = this.MapEntityToDbModel.Invoke(entity);
            return await this.Add(dbmodel, this.DbSet);
        }

        public virtual async Task<long> Count()
        {
            var count = await this.DbSet.LongCountAsync();
            return count;
        }

        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var count = await this.DbSet.Where(filter).LongCountAsync();
            return count;
        }

        public virtual async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await this.Get(id, this.DbSet);
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
        public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.DbSet.Where(filter).AsEnumerable();
            return await Task.FromResult(query);
        }

        public virtual async Task<TEntity> FindFirst(
            Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var entity = await this.DbSet.FirstOrDefaultAsync(filter);
            return entity;
        }

        public virtual async Task<TEntity> GetById(object id) => await this.Get(id, this.DbSet);

        public virtual async Task<object> Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbmodel = this.MapEntityToDbModel.Invoke(entity);
            return await this.Update(dbmodel, this.DbSet);
        }

        public virtual async Task<object> Update(object id, IUpdateExpression<TEntity> update)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            var entity = await this.Get(id, this.DbSet);
            if (entity == null)
            {
                return null;
            }

            // TODO : Updater
            var updater = new Updater<TEntity>(update);
            await updater.Invoke(entity);

            return await this.Update(entity);
        }

        protected async Task<T> Add<T>(T entity, IDbSet<T> set)
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
                return entity;
            }
            else
            {
                return await Task.FromResult(set.Add(entity));
            }
        }

        protected virtual async Task<T> Get<T>(object id, IDbSet<T> set)
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

            return await Task.Run(() => set.Find(id));
        }

        protected async Task<T> Update<T>(T entity, IDbSet<T> set)
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
            return await Task.FromResult(entity);
        }
    }
}
