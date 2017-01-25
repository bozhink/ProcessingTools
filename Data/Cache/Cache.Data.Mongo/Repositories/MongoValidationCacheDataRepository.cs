namespace ProcessingTools.Cache.Data.Mongo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Repositories;
    using Models;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using ProcessingTools.Cache.Data.Common.Contracts.Models;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Factories;

    public class MongoValidationCacheDataRepository : IMongoValidationCacheDataRepository
    {
        private readonly IMongoCollection<ValidatedObject> collection;

        public MongoValidationCacheDataRepository(IMongoDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            string collectionName = CollectionNameFactory.Create<ValidatedObject>();
            this.collection = databaseProvider.Create().GetCollection<ValidatedObject>(
                collectionName,
                new MongoCollectionSettings
                {
                    WriteConcern = WriteConcern.Unacknowledged
                });
        }

        public IEnumerable<string> Keys => this.collection.AsQueryable().Select(o => o.Id);

        public async Task<object> Add(string key, IValidationCacheEntity value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var dbmodel = new ValidatedObject(key, value);
            await this.collection.InsertOneAsync(dbmodel);

            return true;
        }

        public IEnumerable<IValidationCacheEntity> GetAll(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var dbmodel = this.collection.Find(o => o.Id == key).FirstOrDefault();
            return dbmodel?.Values;
        }

        public async Task<object> Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return await this.collection.DeleteOneAsync(o => o.Id == key);
        }

        public async Task<object> Remove(string key, IValidationCacheEntity value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var dbmodel = await this.collection.Find(o => o.Id == key).FirstOrDefaultAsync();
            if (dbmodel == null)
            {
                return false;
            }

            var result = dbmodel.Values.Remove(value);

            var response = await this.collection.ReplaceOneAsync(o => o.Id == key, dbmodel);

            return result && response.IsAcknowledged;
        }

        public Task<long> SaveChanges() => Task.FromResult(0L);
    }
}
