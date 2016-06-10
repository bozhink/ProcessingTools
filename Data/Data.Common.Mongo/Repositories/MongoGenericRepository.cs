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

        public virtual async Task<IQueryable<TEntity>> All(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return (await this.All())
                .Where(filter);
        }

        public virtual async Task<IQueryable<TEntity>> All(Expression<Func<TEntity, object>> sort, int skip, int take)
        {
            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            if (skip < 0)
            {
                throw new ArgumentException(string.Empty, nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentException(string.Empty, nameof(take));
            }

            return (await this.All())
                .OrderBy(sort)
                .Skip(skip)
                .Take(take);
        }

        public virtual async Task<IQueryable<TEntity>> All(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> sort, int skip, int take)
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
                throw new ArgumentException(string.Empty, nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentException(string.Empty, nameof(take));
            }

            return (await this.All())
                .Where(filter)
                .OrderBy(sort)
                .Skip(skip)
                .Take(take);
        }

        public virtual async Task<IQueryable<TEntity>> Query(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sort,
            int skip = 0,
            int take = DefaultPagingConstants.DefaultNumberOfTopItemsToSelect,
            SortOrder sortOrder = SortOrder.Ascending)
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

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            var query = this.Collection.Find(filter: filter);

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    query = query.SortBy(field: sort);
                    break;

                case SortOrder.Descending:
                    query = query.SortByDescending(field: sort);
                    break;

                default:
                    throw new NotImplementedException();
            }

            query = query.Skip(skip).Limit(take);

            var result = await query.ToListAsync();

            return result.AsQueryable();
        }

        public virtual async Task<IQueryable<T>> Query<T>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, T>> projection,
            Expression<Func<TEntity, object>> sort,
            int skip = 0,
            int take = DefaultPagingConstants.DefaultNumberOfTopItemsToSelect,
            SortOrder sortOrder = SortOrder.Ascending)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            var query = this.Collection.Find(filter: filter);

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    query = query.SortBy(field: sort);
                    break;

                case SortOrder.Descending:
                    query = query.SortByDescending(field: sort);
                    break;

                default:
                    throw new NotImplementedException();
            }

            query = query.Skip(skip).Limit(take);

            var result = await query.Project(projection: projection).ToListAsync();

            return result.AsQueryable();
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

        public virtual Task<int> SaveChanges() => Task.FromResult(0);

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

        private FilterDefinition<TEntity> GetFilterById(object id)
        {
            var objectId = new ObjectId(id.ToString());
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            return filter;
        }
    }
}