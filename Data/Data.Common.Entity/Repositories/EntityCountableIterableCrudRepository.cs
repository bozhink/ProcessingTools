namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ProcessingTools.Common.Validation;
    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class EntityCountableIterableCrudRepository<TContext, TEntity> : EntityIterableCrudRepository<TContext, TEntity>, ICountableIterableCrudRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        public EntityCountableIterableCrudRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }

        public virtual async Task<long> Count()
        {
            var count = await this.DbSet.CountAsync();
            return count;
        }

        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> filter)
        {
            DummyValidator.ValidateFilter(filter);

            var count = await this.DbSet.CountAsync(filter);
            return count;
        }
    }
}
