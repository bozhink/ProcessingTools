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

        private readonly UpdateOptions updateOptions;

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

            this.updateOptions = new UpdateOptions
            {
                IsUpsert = true,
                BypassDocumentValidation = false
            };
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

            return await this.collection.UpdateOneAsync(
                Builders<ValidatedObject>.Filter
                    .Eq(o => o.Id, key),
                Builders<ValidatedObject>.Update
                    .AddToSet(o => o.Values, new ValidationCacheEntity(value)),
                this.updateOptions);
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

            var result = dbmodel.Values.Remove(new ValidationCacheEntity(value));

            var response = await this.collection.ReplaceOneAsync(o => o.Id == key, dbmodel);

            return result && response.IsAcknowledged;
        }

        public Task<long> SaveChanges() => Task.FromResult(0L);
    }
}
