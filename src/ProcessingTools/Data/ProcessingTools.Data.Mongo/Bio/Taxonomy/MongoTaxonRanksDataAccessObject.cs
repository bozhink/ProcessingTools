// <copyright file="MongoTaxonRanksDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Bio.Taxonomy.Mongo
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Data.Models.Bio.Taxonomy.Mongo;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// MongoDB implementation of <see cref="ITaxonRanksDataAccessObject"/>.
    /// </summary>
    public class MongoTaxonRanksDataAccessObject : ITaxonRanksDataAccessObject
    {
        private readonly IMongoCollection<TaxonRankItem> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoTaxonRanksDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">MongoDB collection of <see cref="TaxonRankItem"/>.</param>
        public MongoTaxonRanksDataAccessObject(IMongoCollection<TaxonRankItem> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        /// <inheritdoc/>
        public async Task<object> UpsertAsync(ITaxonRankItem item)
        {
            if (item == null)
            {
                return null;
            }

            FilterDefinition<TaxonRankItem> filter = Builders<TaxonRankItem>.Filter.Eq(x => x.Name, item.Name);

            UpdateDefinition<TaxonRankItem> update = Builders<TaxonRankItem>.Update
                .Set(x => x.Name, item.Name)
                .Set(x => x.IsWhiteListed, item.IsWhiteListed)
                .AddToSetEach(x => x.Ranks, item.Ranks);

            UpdateOptions updateOptions = new UpdateOptions
            {
                BypassDocumentValidation = false,
                IsUpsert = true
            };

            var result = await this.collection.UpdateOneAsync(filter, update, updateOptions).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            FilterDefinition<TaxonRankItem> filter = Builders<TaxonRankItem>.Filter.Eq(x => x.Name, name);

            var result = await this.collection.DeleteOneAsync(filter).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public async Task<ITaxonRankItem[]> FindAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return Array.Empty<ITaxonRankItem>();
            }

            Regex re = new Regex("(?i)" + Regex.Escape(filter));
            FilterDefinition<TaxonRankItem> filterDefinition = Builders<TaxonRankItem>.Filter.Regex(x => x.Name, new BsonRegularExpression(re));
            var data = await this.collection.Find(filterDefinition).ToListAsync().ConfigureAwait(false);

            return data?.ToArray<ITaxonRankItem>() ?? Array.Empty<ITaxonRankItem>();
        }

        /// <inheritdoc/>
        public async Task<string[]> GetWhiteListedAsync()
        {
            FilterDefinition<TaxonRankItem> filter = Builders<TaxonRankItem>.Filter.Eq(x => x.IsWhiteListed, true);

            var query = this.collection.Aggregate().Match(filter).Project(x => x.Name);

            var data = await query.ToListAsync().ConfigureAwait(false);

            return data.Distinct().ToArray();
        }

        /// <inheritdoc/>
        public Task<long> SaveChangesAsync() => Task.FromResult(-1L);
    }
}
