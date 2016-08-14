namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using MongoDB.Driver;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Types;
    using ProcessingTools.Data.Common.Mongo.Contracts;

    public class MongoSearchableRepository<TEntity, TDbModel> : MongoRepository<TDbModel>, IMongoSearchableRepository<TEntity>
        where TEntity : class
        where TDbModel : class, TEntity
    {
        public MongoSearchableRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }

        public virtual Task<IQueryable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.Collection.AsQueryable().Where(filter);

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

            return (await this.Find(filter))
                .Select(projection);
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

            var query = this.Collection.AsQueryable().Where(filter);

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

            return (await this.Find(filter, sort, sortOrder, skip, take))
                .Select(projection);
        }

        public virtual Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return Task.Run(() =>
            {
                var entity = this.Collection
                    .AsQueryable()
                    .FirstOrDefault(filter);

                if (entity == null)
                {
                    throw new EntityNotFoundException();
                }

                return entity;
            });
        }

        public virtual Task<T> FindFirst<T>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, T>> projection)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            return Task.Run(() =>
            {
                var entity = this.Collection
                    .AsQueryable()
                    .Where(filter)
                    .Select(projection)
                    .FirstOrDefault();

                if (entity == null)
                {
                    throw new EntityNotFoundException();
                }

                return entity;
            });
        }
    }
}
