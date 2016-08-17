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
    using ProcessingTools.Data.Common.Entity.Contracts;

    public class EntityGenericRepository<TContext, TEntity> : EntityCrudRepository<TContext, TEntity>, IEntityGenericRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        public EntityGenericRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }

        public virtual Task<IQueryable<TEntity>> All() => Task.FromResult(this.DbSet.AsQueryable());

        public virtual async Task<long> Count()
        {
            var count = await this.DbSet.CountAsync();
            return count;
        }

        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var count = await this.DbSet.CountAsync(filter);
            return count;
        }

        public virtual Task<IQueryable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.DbSet.Where(filter);

            return Task.FromResult(query);
        }

        public virtual async Task<IQueryable<T>> Find<T>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, T>> projection)
        {
            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

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
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

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
            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            var query = await this.Find(filter, sort, sortOrder, skip, take);

            return query.Select(projection);
        }

        public virtual async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var entity = await this.DbSet.FirstOrDefaultAsync(filter);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            return entity;
        }

        public virtual async Task<T> FindFirst<T>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, T>> projection)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

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
