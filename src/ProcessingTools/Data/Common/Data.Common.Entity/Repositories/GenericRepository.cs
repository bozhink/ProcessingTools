namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Data.Common.Entity.Contracts.Repositories;
    using ProcessingTools.Data.Contracts.Repositories;

    public class GenericRepository<TContext, TEntity> : IRepository<TEntity>, IGenericRepository<TContext, TEntity>
        where TContext : class, IDbContext
        where TEntity : class
    {
        public GenericRepository(TContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<TEntity>();
        }

        public TContext Context { get; private set; }

        public IDbSet<TEntity> DbSet { get; private set; }

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
            where T : class => this.Context.Set<T>().AsQueryable();

        public virtual object SaveChanges() => this.Context.SaveChanges();

        public virtual async Task<object> SaveChangesAsync() => await this.Context.SaveChangesAsync().ConfigureAwait(false);

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
