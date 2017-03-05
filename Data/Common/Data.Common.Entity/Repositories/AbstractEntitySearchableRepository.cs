namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public abstract class EntitySearchableRepository<TContext, TDbModel, TEntity> : EntityRepository<TContext, TDbModel>, IEntitySearchableRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
        where TDbModel : class, TEntity
    {
        public EntitySearchableRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }

        public virtual IQueryable<TEntity> Query => this.DbSet.AsQueryable<TEntity>();

        // TODO
        public virtual Task<IEnumerable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter) => Task.Run(() =>
        {
            DummyValidator.ValidateFilter(filter);

            var query = this.DbSet.Where(filter).AsEnumerable();
            return query;
        });

        public virtual async Task<TEntity> FindFirst(
            Expression<Func<TEntity, bool>> filter)
        {
            DummyValidator.ValidateFilter(filter);

            var entity = await this.DbSet.FirstOrDefaultAsync(filter);
            return entity;
        }

        public virtual async Task<TEntity> Get(object id) => await this.Get(id, this.DbSet);

        protected virtual Task<T> Get<T>(object id, IDbSet<T> set) where T : class => Task.Run(() =>
        {
            DummyValidator.ValidateId(id);
            DummyValidator.ValidateSet(set);

            var entity = set.Find(id);
            return entity;
        });
    }
}
