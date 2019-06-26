namespace ProcessingTools.Data.Entity.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public abstract class EntityRepository<TContext, TDbModel, TEntity> : EntityRepository<TContext, TDbModel>, IEntityCrudRepository<TEntity>
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
