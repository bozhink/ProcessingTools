namespace ProcessingTools.Data.Entity.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public abstract class EntityRepository<TContext, TEntity> : IEntityRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        protected EntityRepository(IDbContextProvider<TContext> contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.Context = contextProvider.Create();
            this.DbSet = this.GetDbSet<TEntity>();
        }

        ~EntityRepository()
        {
            this.Dispose(false);
        }

        protected DbSet<TEntity> DbSet { get; private set; }

        private TContext Context { get; set; }

        public virtual object SaveChanges() => this.Context.SaveChanges();

        public virtual async Task<object> SaveChangesAsync() => await this.Context.SaveChangesAsync().ConfigureAwait(false);

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

        protected DbSet<T> GetDbSet<T>()
            where T : class
        {
            return this.Context.Set<T>();
        }

        protected EntityEntry<T> GetEntry<T>(T entity)
            where T : class => this.Context.Entry(entity);
    }
}
