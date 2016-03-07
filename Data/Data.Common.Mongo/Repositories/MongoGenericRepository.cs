namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;

    using MongoDB.Driver;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;

    public class MongoGenericRepository<TEntity> : IMongoGenericRepository<TEntity>
    {
        private string collectionName;
        private IMongoDatabase db;

        public MongoGenericRepository(IMongoDatabase db)
        {
            this.db = db;
            this.collectionName = typeof(TEntity).Name.ToLower() + "s";
        }

        public async Task Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var collection = this.db.GetCollection<BsonDocument>(this.collectionName);
            var document = entity.ToBsonDocument();
            await collection.InsertOneAsync(document);
        }

        public async Task<IQueryable<TEntity>> All()
        {
            var collection = db.GetCollection<BsonDocument>(this.collectionName);
            var documents = await collection.Find(new BsonDocument()).ToListAsync();
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

            return (await this.All())
                .Skip(skip)
                .Take(take);
        }

        public async Task Delete(object id)
        {
            var objectId = new ObjectId(id.ToString());
            var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);

            var collection = this.db.GetCollection<BsonDocument>(this.collectionName);
            await collection.DeleteOneAsync(filter);
        }

        public Task Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> Get(object id)
        {
            var objectId = new ObjectId(id.ToString());
            var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);

            var collection = this.db.GetCollection<BsonDocument>(this.collectionName);
            var document = (await collection.FindAsync<BsonDocument>(filter)).FirstOrDefault();
            return BsonSerializer.Deserialize<TEntity>(document);
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}