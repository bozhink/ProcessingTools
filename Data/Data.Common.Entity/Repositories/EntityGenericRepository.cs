namespace ProcessingTools.Data.Common.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Types;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public class EntityGenericRepository<TContext, TEntity> : EntityCountableIterableCrudRepository<TContext, TEntity>, IEntityGenericRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        public EntityGenericRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }

        public virtual Task<IQueryable<TEntity>> Find(Expression<Func<TEntity, bool>> filter)
        {
            DummyValidator.ValidateFilter(filter);

            var query = this.DbSet.Where(filter);
            return Task.FromResult(query);
        }

        public virtual async Task<IQueryable<T>> Find<T>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, T>> projection)
        {
            DummyValidator.ValidateFilter(filter);
            DummyValidator.ValidateProjection(projection);

            var query = await this.Find(filter);
            return query.Select(projection);
        }

        public virtual Task<IQueryable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect)
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
            return Task.FromResult(query);
        }

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

        public virtual async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> filter)
        {
            DummyValidator.ValidateFilter(filter);

            var entity = await this.DbSet.FirstOrDefaultAsync(filter);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            return entity;
        }

        public virtual async Task<T> FindFirst<T>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, T>> projection)
        {
            DummyValidator.ValidateFilter(filter);
            DummyValidator.ValidateProjection(projection);

            var entity = await this.DbSet
                .Where(filter)
                .Select(projection)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            return entity;
        }
    }
}
