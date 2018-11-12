// <copyright file="MongoStringListDataAccessObject{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Bio.Taxonomy.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Models.Contracts;

    /// <summary>
    /// Generic MongoDB implementation of <see cref="IStringListDataAccessObject"/>.
    /// </summary>
    /// <typeparam name="T">Type of the database model.</typeparam>
    public class MongoStringListDataAccessObject<T> : IStringListDataAccessObject
        where T : IStringListItem
    {
        private readonly IMongoCollection<T> collection;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoStringListDataAccessObject{T}"/> class.
        /// </summary>
        /// <param name="collection">MongoDB collection of the string list items.</param>
        public MongoStringListDataAccessObject(IMongoCollection<T> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<string, T>().ForMember(m => m.Content, o => o.MapFrom(s => s));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<object> InsertOneAsync(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                return null;
            }

            FilterDefinition<T> filter = Builders<T>.Filter.Eq(x => x.Content, item);
            UpdateDefinition<T> update = Builders<T>.Update.Set(x => x.Content, item);
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

            FilterDefinition<T> filter = Builders<T>.Filter.In(x => x.Content, items);

            var data = await this.collection.Find(filter).Project(e => new { e.Content }).ToListAsync().ConfigureAwait(false);

            var itemsToInsert = items.Except(data.Select(e => e.Content))
                .Distinct()
                .Select(this.mapper.Map<string, T>)
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

            FilterDefinition<T> filter = Builders<T>.Filter.Eq(x => x.Content, item);
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

            FilterDefinition<T> filter = Builders<T>.Filter.In(x => x.Content, items);
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

            FilterDefinition<T> filterDefinition = Builders<T>.Filter.Text(filter, searchOptions);
            SortDefinition<T> sortDefinition = Builders<T>.Sort.MetaTextScore("textScore");

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
            FilterDefinition<T> filter = Builders<T>.Filter.Empty;

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
