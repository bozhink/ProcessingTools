namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Common.Validation;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public class EntityCrudRepository<TContext, TEntity> : EntityRepository<TContext, TEntity>, IEntityCrudRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        public EntityCrudRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }

        public virtual async Task<object> Add(TEntity entity) => await this.Add(entity, this.DbSet);

        public virtual async Task<object> Delete(TEntity entity) => await this.Delete(entity, this.DbSet);

        public virtual async Task<object> Delete(object id) => await this.Delete(id, this.DbSet);

        public virtual async Task<TEntity> Get(object id) => await this.Get(id, this.DbSet);

        public virtual async Task<object> Update(TEntity entity) => await this.Update(entity, this.DbSet);
    }
}
