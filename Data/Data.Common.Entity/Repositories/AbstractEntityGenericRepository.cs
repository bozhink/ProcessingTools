namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Common.Validation;
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

        public async Task<long> Count()
        {
            var count = await this.DbSet.LongCountAsync();
            return count;
        }

        public async Task<long> Count(Expression<Func<TEntity, bool>> filter)
        {
            DummyValidator.ValidateFilter(filter);

            var count = await this.DbSet.Where(filter).LongCountAsync();
            return count;
        }
    }
}
