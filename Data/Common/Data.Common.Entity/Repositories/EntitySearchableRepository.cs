namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System.Data.Entity;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public class EntitySearchableRepository<TContext, TEntity> : EntitySearchableRepository<TContext, TEntity, TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        public EntitySearchableRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }
    }
}
