namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Common.Entity.Contracts;

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

        protected IDbSet<TEntity> DbSet { get; private set; }

        private TContext Context { get; set; }

        public virtual object SaveChanges() => this.Context.SaveChanges();

        public virtual async Task<object> SaveChangesAsync() => await this.Context.SaveChangesAsync();

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

        protected IDbSet<T> GetDbSet<T>()
            where T : class
        {
            return this.Context.Set<T>();
        }

        protected DbEntityEntry<T> GetEntry<T>(T entity)
            where T : class => this.Context.Entry(entity);
    }
}
