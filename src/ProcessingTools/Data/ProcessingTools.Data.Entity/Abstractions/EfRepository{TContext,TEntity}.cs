namespace ProcessingTools.Data.Entity.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class EfRepository<TContext, TEntity> : IEfRepository<TContext, TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        public EfRepository(TContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<TEntity>();
        }

        public TContext Context { get; }

        public DbSet<TEntity> DbSet { get; }

        public virtual void Add(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public virtual void Delete(object id)
        {
            var entity = this.Get(id);
            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public virtual void Detach(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        public virtual TEntity Get(object id)
        {
            return this.DbSet.Find(id);
        }

        public IQueryable<TEntity> Queryable() => this.DbSet.AsQueryable();

        public IQueryable<T> Queryable<T>()
            where T : class
        {
            return this.Context.Set<T>().AsQueryable();
        }

        public virtual object SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public virtual async Task<object> SaveChangesAsync()
        {
            return await this.Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
