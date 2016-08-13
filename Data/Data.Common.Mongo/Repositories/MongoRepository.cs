namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using Contracts;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using MongoDB.Bson;

    public abstract class MongoRepository<TEntity> : IMongoRepository<TEntity>
        where TEntity : class
    {
        private readonly IMongoDatabase db;
        private string collectionName;

        public MongoRepository(IMongoDatabaseProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.db = provider.Create();
            this.CollectionName = this.GenerateCollectionName();
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

        private string GenerateCollectionName()
        {
            string name = typeof(TEntity).Name.ToLower();
            int nameLength = name.Length;
            if (name.ToCharArray()[nameLength - 1] == 'y')
            {
                return $"{name.Substring(0, nameLength - 1)}ies";
            }
            else
            {
                return $"{name}s";
            }
        }
    }
}
