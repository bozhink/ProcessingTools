namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public class EntityGenericRepository<TContext, TEntity> : EntityRepository<TContext, TEntity, TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        public EntityGenericRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }

        protected override Func<TEntity, TEntity> MapEntityToDbModel => e => e;

        public override async Task<object> Add(TEntity entity) => await this.Add(entity, this.DbSet);

        public override async Task<object> Update(TEntity entity) => await this.Update(entity, this.DbSet);
    }
}
