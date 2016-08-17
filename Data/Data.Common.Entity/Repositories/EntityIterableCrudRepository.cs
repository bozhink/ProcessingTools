namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Common.Entity.Contracts;

    public class EntityIterableCrudRepository<TContext, TEntity> : EntityCrudRepository<TContext, TEntity>, IEntityIterableCrudRepository<TEntity>, IDisposable
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
