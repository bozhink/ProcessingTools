// <copyright file="MongoValidationCacheDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.DataAccess.Cache;
    using ProcessingTools.Contracts.DataAccess.Models.Cache;
    using ProcessingTools.Contracts.Models.Cache;
    using ProcessingTools.Data.Models.Mongo.Cache;

    /// <summary>
    /// MongoDB implementation of <see cref="IValidationCacheDataAccessObject"/>.
    /// </summary>
    public class MongoValidationCacheDataAccessObject : IValidationCacheDataAccessObject
    {
        private readonly IMongoCollection<ValidatedObject> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoValidationCacheDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">Collection in the MongoDB database. It is supposed that this collection is with WriteConcern.Unacknowledged.</param>
        public MongoValidationCacheDataAccessObject(IMongoCollection<ValidatedObject> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        /// <inheritdoc/>
        public async Task<bool> AddAsync(string key, IValidationCacheModel model)
        {
            if (string.IsNullOrWhiteSpace(key) || model is null)
            {
                return false;
            }

            FilterDefinition<ValidatedObject> filter = Builders<ValidatedObject>.Filter.Eq(o => o.Id, key);
            UpdateDefinition<ValidatedObject> update = Builders<ValidatedObject>.Update.AddToSet(o => o.Values, new ValidationCacheEntity(model));
            UpdateOptions updateOptions = new UpdateOptions
            {
                IsUpsert = true,
                BypassDocumentValidation = false,
            };

            var response = await this.collection.UpdateOneAsync(filter, update, updateOptions).ConfigureAwait(false);

            return response.IsAcknowledged;
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(string key, IValidationCacheModel model)
        {
            if (string.IsNullOrWhiteSpace(key) || model is null)
            {
                return false;
            }

            var dbmodel = await this.collection.Find(o => o.Id == key).FirstOrDefaultAsync().ConfigureAwait(false);
            if (dbmodel == null)
            {
                return false;
            }

            var result = dbmodel.Values.Remove(new ValidationCacheEntity(model));

            var response = await this.collection.ReplaceOneAsync(o => o.Id == key, dbmodel).ConfigureAwait(false);

            return result && response.IsAcknowledged;
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            FilterDefinition<ValidatedObject> filter = Builders<ValidatedObject>.Filter.Eq(o => o.Id, key);

            var response = await this.collection.DeleteOneAsync(filter).ConfigureAwait(false);

            return response.IsAcknowledged;
        }

        /// <inheritdoc/>
        public async Task<bool> ClearCacheAsync()
        {
            FilterDefinition<ValidatedObject> filter = Builders<ValidatedObject>.Filter.Empty;

            var response = await this.collection.DeleteManyAsync(filter).ConfigureAwait(false);

            return response.IsAcknowledged;
        }

        /// <inheritdoc/>
        public async Task<IList<IValidationCacheDataTransferObject>> GetAllForKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return Array.Empty<IValidationCacheDataTransferObject>();
            }

            FilterDefinition<ValidatedObject> filter = Builders<ValidatedObject>.Filter.Eq(o => o.Id, key);
            var query = this.collection.Find(filter);

            var entity = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            var data = entity?.Values ?? Array.Empty<ValidationCacheEntity>();

            foreach (var item in data)
            {
                item.Key = key;
            }

            return data.ToArray<IValidationCacheDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IValidationCacheDataTransferObject> GetLastForKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            var query = this.collection.Aggregate(new AggregateOptions { AllowDiskUse = true }).Match(o => o.Id == key)
                .Project(o => new ValidatedObjectProjectAggregation { Key = o.Id, Values = o.Values })
                .Unwind(o => o.Values)
                .As<ValidatedObjectUnwindAggregation>()
                .SortByDescending(o => o.Values.LastUpdate)
                .Skip(0)
                .Limit(1);

            var entity = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            var result = entity?.Values;

            if (result != null)
            {
                result.Key = key;
            }

            return result;
        }
    }
}
