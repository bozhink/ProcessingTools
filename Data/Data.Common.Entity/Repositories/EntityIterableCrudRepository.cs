namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class EntityIterableCrudRepository<TContext, TEntity> : EntityCrudRepository<TContext, TEntity>, IIterableCrudRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        public EntityIterableCrudRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }

        public virtual Task<IQueryable<TEntity>> All() => Task.FromResult(this.DbSet.AsQueryable());
    }
}
