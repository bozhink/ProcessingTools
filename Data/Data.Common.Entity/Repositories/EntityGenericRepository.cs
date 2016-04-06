namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Common.Entity.Contracts;

    public class EntityGenericRepository<TContext, TEntity> : IEntityGenericRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        public EntityGenericRepository(IDbContextProvider<TContext> contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.Context = contextProvider.Create();
            this.DbSet = this.Context.Set<TEntity>();
        }

        protected IDbSet<TEntity> DbSet { get; set; }

        protected TContext Context { get; set; }

        public virtual Task<IQueryable<TEntity>> All()
        {
            return Task.FromResult(this.DbSet.AsQueryable());
        }

        public Task<IQueryable<TEntity>> All(int skip, int take)
        {
            return Task.FromResult(this.DbSet.AsQueryable().Skip(skip).Take(take));
        }

        public virtual Task<TEntity> Get(object id)
        {
            return Task.FromResult(this.DbSet.Find(id));
        }

        public virtual Task Add(TEntity entity)
        {
            return Task.Run(() =>
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
            });
        }

        public virtual Task Update(TEntity entity)
        {
            return Task.Run(() =>
            {
                var entry = this.Context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    this.DbSet.Attach(entity);
                }

                entry.State = EntityState.Modified;
            });
        }

        public virtual Task Delete(TEntity entity)
        {
            return Task.Run(() =>
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
            });
        }

        public virtual async Task Delete(object id)
        {
            var entity = await this.Get(id);
            if (entity != null)
            {
                await this.Delete(entity);
            }
        }

        public Task<int> SaveChanges()
        {
            return this.Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context.Dispose();
            }
        }
    }
}