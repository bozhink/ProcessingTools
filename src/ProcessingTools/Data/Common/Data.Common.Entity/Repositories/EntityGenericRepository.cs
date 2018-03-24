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

        public override async Task<object> AddAsync(TEntity entity) => await this.AddAsync(entity, this.DbSet).ConfigureAwait(false);

        public override async Task<object> UpdateAsync(TEntity entity) => await this.UpdateAsync(entity, this.DbSet).ConfigureAwait(false);
    }
}
