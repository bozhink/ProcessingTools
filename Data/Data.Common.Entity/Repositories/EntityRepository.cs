namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;

    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public abstract class EntityRepository<TContext, TEntity> : IRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        public EntityRepository(IDbContextProvider<TContext> contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.Context = contextProvider.Create();
            this.DbSet = this.Context.Set<TEntity>();
        }

        protected IDbSet<TEntity> DbSet { get; private set; }

        private TContext Context { get; set; }

        public virtual async Task<long> SaveChanges()
        {
            long result = await this.Context.SaveChangesAsync();
            return result;
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

        protected DbEntityEntry<TEntity> GetEntry(TEntity entity) => this.Context.Entry(entity);
    }
}
