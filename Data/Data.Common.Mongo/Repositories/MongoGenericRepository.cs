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

    using ProcessingTools.Data.Common.Extensions;
    using ProcessingTools.Data.Common.Mongo.Contracts;

    public class MongoGenericRepository<TEntity> : IMongoGenericRepository<TEntity>
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
                    throw new ArgumentNullException("CollectionName should not be null or empty.", "CollectionName");
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

        private IMongoCollection<TEntity> Collection => this.db.GetCollection<TEntity>(this.CollectionName);

        public virtual async Task Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.Collection.InsertOneAsync(entity);
        }

        public virtual async Task<IQueryable<TEntity>> All()
        {
            var entities = await this.Collection
                .Find(new BsonDocument())
                .ToListAsync();

            return entities.AsQueryable();
        }

        public virtual async Task<IQueryable<TEntity>> All(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var entities = await this.Collection
               .Find(filter)
               .ToListAsync();

            return entities.AsQueryable();
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

            var entities = await this.Collection
                .Find(new BsonDocument())
                .SortBy(sort)
                .Skip(skip)
                .Limit(take)
                .ToListAsync();

            return entities.AsQueryable();
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

            var entities = await this.Collection
                .Find(filter)
                .SortBy(sort)
                .Skip(skip)
                .Limit(take)
                .ToListAsync();

            return entities.AsQueryable();
        }

        public virtual async Task Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            await this.Collection.DeleteOneAsync(filter);
        }

        public virtual async Task Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = entity.GetIdValue<BsonIdAttribute>();
            await this.Delete(id);
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

        public virtual Task<int> SaveChanges()
        {
            return Task.FromResult(0);
        }

        public virtual async Task Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = entity.GetIdValue<BsonIdAttribute>();
            var filter = this.GetFilterById(id);
            var result = await this.Collection.ReplaceOneAsync(filter, entity);
        }

        private FilterDefinition<TEntity> GetFilterById(object id)
        {
            var objectId = new ObjectId(id.ToString());
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            return filter;
        }
    }
}