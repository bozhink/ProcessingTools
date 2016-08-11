namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Types;
    using ProcessingTools.Data.Common.Extensions;
    using ProcessingTools.Data.Common.Mongo.Contracts;

    public class MongoGenericRepository<TEntity> : IMongoGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly IMongoDatabase db;
        private string collectionName;

        public MongoGenericRepository(IMongoDatabaseProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.db = provider.Create();
            this.CollectionName = typeof(TEntity).Name;
        }

        protected IMongoCollection<TEntity> Collection => this.db.GetCollection<TEntity>(this.CollectionName);

        private string CollectionName
        {
            get
            {
                return this.collectionName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.CollectionName));
                }

                string name = value.ToLower();
                int nameLength = name.Length;
                if (name.ToCharArray()[nameLength - 1] == 'y')
                {
                    this.collectionName = $"{name.Substring(0, nameLength - 1)}ies";
                }
                else
                {
                    this.collectionName = $"{name}s";
                }
            }
        }

        public virtual async Task<object> Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.Collection.InsertOneAsync(entity);
            return entity;
        }

        public virtual Task<IQueryable<TEntity>> All()
        {
            return Task.FromResult<IQueryable<TEntity>>(this.Collection.AsQueryable());
        }

        public virtual async Task<long> Count()
        {
            var count = await this.Collection.CountAsync("{}");
            return count;
        }

        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var count = await this.Collection.CountAsync(filter);
            return count;
        }

        public virtual async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var result = await this.Collection.DeleteOneAsync(filter);
            return result;
        }

        public virtual async Task<object> Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = entity.GetIdValue<BsonIdAttribute>();
            return await this.Delete(id);
        }

        public virtual async Task<TEntity> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var entity = await this.Collection.Find(filter).FirstOrDefaultAsync();
            return entity;
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

        public virtual async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var entity = await this.Collection
                .Find(filter)
                .FirstOrDefaultAsync();

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

            var entity = await this.Collection
                .Find(filter)
                .Project(projection)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            return entity;
        }

        public virtual async Task<object> Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = entity.GetIdValue<BsonIdAttribute>();
            var filter = this.GetFilterById(id);
            var result = await this.Collection.ReplaceOneAsync(filter, entity);
            return result;
        }

        public virtual Task<int> SaveChanges() => Task.FromResult(0);

        private FilterDefinition<TEntity> GetFilterById(object id)
        {
            var objectId = new ObjectId(id.ToString());
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            return filter;
        }
    }
}