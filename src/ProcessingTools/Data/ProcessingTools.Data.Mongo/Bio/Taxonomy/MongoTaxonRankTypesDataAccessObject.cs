// <copyright file="MongoTaxonRankTypesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Data.Models.Mongo.Bio.Taxonomy;

    /// <summary>
    /// MongoDB implementation of <see cref="ITaxonRankTypesDataAccessObject"/>.
    /// </summary>
    public class MongoTaxonRankTypesDataAccessObject : ITaxonRankTypesDataAccessObject
    {
        private readonly IMongoCollection<TaxonRankTypeItem> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoTaxonRankTypesDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">MongoDB collection of <see cref="TaxonRankTypeItem"/>.</param>
        public MongoTaxonRankTypesDataAccessObject(IMongoCollection<TaxonRankTypeItem> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        /// <inheritdoc/>
        public async Task<object> SeedFromTaxonRankTypeEnumAsync()
        {
            var entities = Enum.GetValues(typeof(TaxonRankType)).Cast<TaxonRankType>()
               .Select(rank => new TaxonRankTypeItem
               {
                   RankType = rank
               })
               .ToList();

            var options = new InsertManyOptions
            {
                IsOrdered = false,
                BypassDocumentValidation = false
            };

            await this.collection.InsertManyAsync(entities, options).ConfigureAwait(false);

            return entities;
        }

        /// <inheritdoc/>
        public Task<long> SaveChangesAsync() => Task.FromResult(-1L);
    }
}
