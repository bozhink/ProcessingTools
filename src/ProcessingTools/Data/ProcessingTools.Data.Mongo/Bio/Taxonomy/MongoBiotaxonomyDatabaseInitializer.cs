// <copyright file="MongoBiotaxonomyDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Bio.Taxonomy
{
    using System;
    using System.Threading.Tasks;
    using Common;
    using Common.Contracts;
    using Contracts.Bio.Taxonomy;
    using Models.Mongo.Bio.Taxonomy;
    using MongoDB.Driver;

    /// <summary>
    /// Implementation of <see cref="IBiotaxonomyDatabaseInitializer"/>.
    /// </summary>
    public class MongoBiotaxonomyDatabaseInitializer : IBiotaxonomyDatabaseInitializer
    {
        private readonly IMongoDatabase db;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoBiotaxonomyDatabaseInitializer"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        public MongoBiotaxonomyDatabaseInitializer(IMongoDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            this.db = databaseProvider.Create();
        }

        /// <inheritdoc/>
        public async Task<object> InitializeAsync()
        {
            await this.CreateIndicesToTaxonRankCollection().ConfigureAwait(false);
            await this.CreateIndicesToTaxonRankTypesCollection().ConfigureAwait(false);
            await this.CreateIndicesToBlackListCollection().ConfigureAwait(false);

            return true;
        }

        private async Task<object> CreateIndicesToTaxonRankCollection()
        {
            string collectionName = MongoCollectionNameFactory.Create<TaxonRankItem>();

            var collection = this.db.GetCollection<TaxonRankItem>(collectionName);

            var indexOptions = new CreateIndexOptions
            {
                Background = false,
                Unique = false,
                Sparse = false
            };

            var result = await collection.Indexes
                .CreateOneAsync(new CreateIndexModel<TaxonRankItem>(
                    Builders<TaxonRankItem>.IndexKeys
                        .Ascending(t => t.Name),
                    indexOptions))
                .ConfigureAwait(false);

            return result;
        }

        private async Task<object> CreateIndicesToTaxonRankTypesCollection()
        {
            string collectionName = MongoCollectionNameFactory.Create<TaxonRankTypeItem>();

            var collection = this.db.GetCollection<TaxonRankTypeItem>(collectionName);

            var indexOptions = new CreateIndexOptions
            {
                Background = false,
                Unique = true,
                Sparse = false
            };

            await collection.Indexes
                .CreateOneAsync(new CreateIndexModel<TaxonRankTypeItem>(
                    Builders<TaxonRankTypeItem>.IndexKeys.Ascending(t => t.RankType),
                    indexOptions))
                .ConfigureAwait(false);

            var result = await collection.Indexes
                .CreateOneAsync(new CreateIndexModel<TaxonRankTypeItem>(
                    Builders<TaxonRankTypeItem>.IndexKeys.Ascending(t => t.Name),
                    indexOptions))
                .ConfigureAwait(false);

            return result;
        }

        private async Task<object> CreateIndicesToBlackListCollection()
        {
            string collectionName = MongoCollectionNameFactory.Create<BlackListItem>();

            var collection = this.db.GetCollection<BlackListItem>(collectionName);

            var indexOptions = new CreateIndexOptions
            {
                Background = false,
                Unique = true,
                Sparse = false
            };

            var result = await collection.Indexes
                .CreateOneAsync(new CreateIndexModel<BlackListItem>(
                    Builders<BlackListItem>.IndexKeys.Ascending(t => t.Content),
                    indexOptions))
                .ConfigureAwait(false);

            return result;
        }
    }
}
