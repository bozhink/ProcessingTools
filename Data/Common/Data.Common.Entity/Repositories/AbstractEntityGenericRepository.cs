// TODO: Delete
namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using Contracts;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public abstract class EntityGenericRepository<TContext, TDbModel, TEntity> : EntityCrudRepository<TContext, TDbModel, TEntity>, IEntityGenericRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
        where TDbModel : class, TEntity
    {
        public EntityGenericRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }
    }
}
