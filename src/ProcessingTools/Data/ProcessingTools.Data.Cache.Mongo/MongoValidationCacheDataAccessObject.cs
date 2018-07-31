// <copyright file="MongoValidationCacheDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Cache.Mongo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.Cache;
    using ProcessingTools.Data.Models.Cache.Mongo;
    using ProcessingTools.Data.Models.Contracts.Cache;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Cache;

    /// <summary>
    /// MongoDB implementation of <see cref="IValidationCacheDataAccessObject"/>.
    /// </summary>
    public class MongoValidationCacheDataAccessObject : IValidationCacheDataAccessObject
    {
        private readonly IMongoCollection<ValidatedObject> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoValidationCacheDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        public MongoValidationCacheDataAccessObject(IMongoDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            string collectionName = MongoCollectionNameFactory.Create<ValidatedObject>();
            var settings = new MongoCollectionSettings
            {
                WriteConcern = WriteConcern.Unacknowledged
            };

            this.collection = databaseProvider.Create().GetCollection<ValidatedObject>(collectionName, settings);
        }

        /// <inheritdoc/>
        public async Task<object> AddAsync(string key, IValidationCacheModel model)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            FilterDefinition<ValidatedObject> filter = Builders<ValidatedObject>.Filter.Eq(o => o.Id, key);
            UpdateDefinition<ValidatedObject> update = Builders<ValidatedObject>.Update.AddToSet(o => o.Values, new ValidationCacheEntity(model));
            UpdateOptions updateOptions = new UpdateOptions
            {
                IsUpsert = true,
                BypassDocumentValidation = false
            };

            return await this.collection.UpdateOneAsync(filter, update, updateOptions).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<object> RemoveAsync(string key, IValidationCacheModel model)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
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
        public async Task<object> RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            FilterDefinition<ValidatedObject> filter = Builders<ValidatedObject>.Filter.Eq(o => o.Id, key);
            return await this.collection.DeleteOneAsync(filter).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<object> ClearCacheAsync()
        {
            FilterDefinition<ValidatedObject> filter = Builders<ValidatedObject>.Filter.Empty;
            return await this.collection.DeleteManyAsync(filter).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IValidationCacheDataModel[]> GetAllForKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            FilterDefinition<ValidatedObject> filter = Builders<ValidatedObject>.Filter.Eq(o => o.Id, key);
            var query = this.collection.Find(filter);

            var entity = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            var data = entity?.Values ?? Array.Empty<ValidationCacheEntity>();

            foreach (var item in data)
            {
                item.Key = key;
            }

            return data.ToArray<IValidationCacheDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IValidationCacheDataModel> GetLastForKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
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

        /// <inheritdoc/>
        public async Task<IValidationCacheDataModel[]> SelectAsync(string filter, SortOrder sortOrder, int skip, int take)
        {
            var query = this.collection.Aggregate(new AggregateOptions { AllowDiskUse = true })
                .Project(o => new ValidatedObjectProjectAggregation { Key = o.Id, Values = o.Values })
                .Unwind(o => o.Values)
                .As<ValidatedObjectUnwindAggregation>();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Match(o => o.Values.Content.Contains(filter));
            }

            switch (sortOrder)
            {
                case SortOrder.Descending:
                    query = query.SortByDescending(o => o.Values.Content).ThenByDescending(o => o.Values.LastUpdate);
                    break;
                default:
                    query = query.SortBy(o => o.Values.Content).ThenBy(o => o.Values.LastUpdate);
                    break;
            }

            query = query.Skip(skip).Limit(take);

            var data = await query.ToListAsync().ConfigureAwait(false);

            foreach (var item in data)
            {
                item.Values.Key = item.Key;
            }

            return data.Select(i => i.Values).ToArray<IValidationCacheDataModel>();
        }

        /// <inheritdoc/>
        public Task<long> CountAsync() => this.collection.CountDocumentsAsync(Builders<ValidatedObject>.Filter.Empty);
    }
}
