// <copyright file="MongoBlackListDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Bio.Taxonomy.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Models;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;

    /// <summary>
    /// MongoDB implementation of <see cref="IBlackListDataAccessObject"/>.
    /// </summary>
    public class MongoBlackListDataAccessObject : IBlackListDataAccessObject
    {
        private readonly IMongoCollection<BlackListItem> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoBlackListDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">MongoDB collection of the black list items.</param>
        public MongoBlackListDataAccessObject(IMongoCollection<BlackListItem> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        /// <inheritdoc/>
        public async Task<object> InsertOneAsync(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                return null;
            }

            FilterDefinition<BlackListItem> filter = Builders<BlackListItem>.Filter.Eq(x => x.Content, item);
            UpdateDefinition<BlackListItem> update = Builders<BlackListItem>.Update.Set(x => x.Content, item);
            UpdateOptions updateOptions = new UpdateOptions
            {
                IsUpsert = true,
                BypassDocumentValidation = false
            };

            var result = await this.collection.UpdateOneAsync(filter, update, updateOptions).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public async Task<object> InsertManyAsync(IEnumerable<string> items)
        {
            if (items == null || !items.Any())
            {
                return null;
            }

            FilterDefinition<BlackListItem> filter = Builders<BlackListItem>.Filter.In(x => x.Content, items);

            var data = await this.collection.Find(filter).Project(e => new { e.Content }).ToListAsync().ConfigureAwait(false);

            var itemsToInsert = items.Except(data.Select(e => e.Content))
                .Distinct()
                .Select(e => new BlackListItem
                {
                    Content = e
                })
                .ToList();

            InsertManyOptions insertOptions = new InsertManyOptions
            {
                IsOrdered = false,
                BypassDocumentValidation = false
            };

            await this.collection.InsertManyAsync(itemsToInsert, insertOptions).ConfigureAwait(false);

            return itemsToInsert.Count;
        }

        /// <inheritdoc/>
        public async Task<object> DeleteOneAsync(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                return null;
            }

            FilterDefinition<BlackListItem> filter = Builders<BlackListItem>.Filter.Eq(x => x.Content, item);
            DeleteOptions options = new DeleteOptions
            {
            };

            var result = await this.collection.DeleteOneAsync(filter, options).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public async Task<object> DeleteManyAsync(IEnumerable<string> items)
        {
            if (items == null || !items.Any())
            {
                return null;
            }

            FilterDefinition<BlackListItem> filter = Builders<BlackListItem>.Filter.In(x => x.Content, items);
            DeleteOptions options = new DeleteOptions
            {
            };

            var result = await this.collection.DeleteManyAsync(filter, options).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public async Task<string[]> FindAsync(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return Array.Empty<string>();
            }

            TextSearchOptions searchOptions = new TextSearchOptions
            {
                CaseSensitive = false,
                DiacriticSensitive = false
            };

            FilterDefinition<BlackListItem> filterDefinition = Builders<BlackListItem>.Filter.Text(filter, searchOptions);
            SortDefinition<BlackListItem> sortDefinition = Builders<BlackListItem>.Sort.MetaTextScore("textScore");

            var query = this.collection.Find(filterDefinition).Sort(sortDefinition).Project(e => new { e.Content });

            var data = await query.ToListAsync().ConfigureAwait(false);

            if (data == null || !data.Any())
            {
                return Array.Empty<string>();
            }

            return data.Select(e => e.Content).ToArray();
        }

        /// <inheritdoc/>
        public async Task<string[]> GetAllAsync()
        {
            FilterDefinition<BlackListItem> filter = Builders<BlackListItem>.Filter.Empty;

            var query = this.collection.Find(filter).Project(e => new { e.Content });
            var data = await query.ToListAsync().ConfigureAwait(false);

            if (data == null || !data.Any())
            {
                return Array.Empty<string>();
            }

            return data.Select(e => e.Content).ToArray();
        }

        /// <inheritdoc/>
        public Task<long> SaveChangesAsync() => Task.FromResult(-1L);
    }
}
