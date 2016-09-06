namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Types;
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

        public virtual Task<IQueryable<TEntity>> All() => Task.FromResult(this.DbSet.AsQueryable<TEntity>());

        public virtual Task<IQueryable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter) => Task.Run(() =>
        {
            DummyValidator.ValidateFilter(filter);

            var query = this.DbSet.Where(filter);
            return query;
        });

        public virtual Task<IQueryable<Tout>> Find<Tout>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, Tout>> projection) => Task.Run(() =>
        {
            DummyValidator.ValidateFilter(filter);
            DummyValidator.ValidateProjection(projection);

            var query = this.DbSet.Where(filter).Select(projection);
            return query;
        });

        public virtual Task<IQueryable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect) => Task.Run(() =>
        {
            DummyValidator.ValidateFilter(filter);
            DummyValidator.ValidateSort(sort);
            DummyValidator.ValidateSkip(skip);
            DummyValidator.ValidateTake(take);

            var query = this.DbSet.Where(filter);

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    query = query.OrderBy(sort);
                    break;

                case SortOrder.Descending:
                    query = query.OrderByDescending(sort);
                    break;

                default:
                    throw new NotImplementedException();
            }

            query = query.Skip(skip).Take(take);
            return query;
        });

        public virtual async Task<IQueryable<T>> Find<T>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, T>> projection,
            Expression<Func<TEntity, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect)
        {
            DummyValidator.ValidateFilter(filter);
            DummyValidator.ValidateProjection(projection);
            DummyValidator.ValidateSort(sort);
            DummyValidator.ValidateSkip(skip);
            DummyValidator.ValidateTake(take);

            var query = await this.Find(filter, sort, sortOrder, skip, take);
            return query.Select(projection);
        }

        public virtual async Task<TEntity> FindFirst(
            Expression<Func<TEntity, bool>> filter)
        {
            DummyValidator.ValidateFilter(filter);

            var entity = await this.DbSet.FirstOrDefaultAsync(filter);
            return entity;
        }

        public virtual async Task<Tout> FindFirst<Tout>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, Tout>> projection)
        {
            DummyValidator.ValidateFilter(filter);
            DummyValidator.ValidateProjection(projection);

            var entity = await this.DbSet
                .Where(filter)
                .Select(projection)
                .FirstOrDefaultAsync();
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
