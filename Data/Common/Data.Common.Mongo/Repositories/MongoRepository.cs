namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using Contracts;
    using Contracts.Repositories;
    using Factories;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public abstract class MongoRepository<TEntity> : IMongoRepository<TEntity>
        where TEntity : class
    {
        private readonly IMongoDatabase db;
        private string collectionName;

        public MongoRepository(IMongoDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            this.db = databaseProvider.Create();
            this.CollectionName = CollectionNameFactory.Create<TEntity>();
        }

        protected IMongoCollection<TEntity> Collection => this.db.GetCollection<TEntity>(this.CollectionName);

        protected string CollectionName
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

                this.collectionName = value;
            }
        }

        protected FilterDefinition<TEntity> GetFilterById(object id)
        {
            var objectId = new ObjectId(id.ToString());
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            return filter;
        }
    }
}
