namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
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
                throw new ArgumentNullException("provider");
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

        private IMongoCollection<BsonDocument> Collection => this.db.GetCollection<BsonDocument>(this.CollectionName);

        public async Task Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var document = entity.ToBsonDocument();
            await this.Collection.InsertOneAsync(document);
        }

        public async Task<IQueryable<TEntity>> All()
        {
            var documents = await this.Collection.Find(new BsonDocument()).ToListAsync();
            var entities = documents.Select(d => BsonSerializer.Deserialize<TEntity>(d));
            return entities.AsQueryable();
        }

        public async Task<IQueryable<TEntity>> All(int skip, int take)
        {
            if (skip < 0)
            {
                throw new ArgumentException("Skip should be non-negative.", "skip");
            }

            if (take < 1)
            {
                throw new ArgumentException("Take should be greater than zero.", "take");
            }

            var documents = await this.Collection.Find(new BsonDocument()).Skip(skip).Limit(take).ToListAsync();
            var entities = documents.Select(d => BsonSerializer.Deserialize<TEntity>(d));
            return entities.AsQueryable();
        }

        public async Task Delete(object id)
        {
            var filter = this.GetFilterById(id);
            await this.Collection.DeleteOneAsync(filter);
        }

        public async Task Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var id = entity.GetIdValue<BsonIdAttribute>();
            await this.Delete(id);
        }

        public async Task<TEntity> Get(object id)
        {
            var filter = this.GetFilterById(id);
            var document = (await this.Collection.FindAsync<BsonDocument>(filter)).FirstOrDefault();
            return BsonSerializer.Deserialize<TEntity>(document);
        }

        public Task<int> SaveChanges()
        {
            return Task.Run(() => 0);
        }

        public async Task Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var updateBuilder = Builders<BsonDocument>.Update;
            entity.GetType()
                .GetProperties()?.ToList()
                .ForEach(p => updateBuilder.Set(p.Name, p.GetValue(entity).ToString()));

            var id = entity.GetIdValue<BsonIdAttribute>();
            var filter = this.GetFilterById(id);
            var update = updateBuilder.CurrentDate("lastModified");
            var result = await this.Collection.UpdateOneAsync(filter, update);
        }

        private FilterDefinition<BsonDocument> GetFilterById(object id)
        {
            var objectId = new ObjectId(id.ToString());
            var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
            return filter;
        }
    }
}