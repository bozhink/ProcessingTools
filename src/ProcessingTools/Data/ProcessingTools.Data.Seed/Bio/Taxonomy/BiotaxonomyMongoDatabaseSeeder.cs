// <copyright file="BiotaxonomyMongoDatabaseSeeder.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Seed.Bio.Taxonomy
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Bio.Taxonomy;

    /// <summary>
    /// Biotaxonomy MongoDB Database Seeder.
    /// </summary>
    public class BiotaxonomyMongoDatabaseSeeder : IBiotaxonomyMongoDatabaseSeeder
    {
        private readonly ITaxonRanksDataAccessObject mongoTaxonRanksDataAccessObject;
        private readonly ITaxonRanksDataAccessObject xmlTaxonRanksDataAccessObject;
        private readonly IBlackListDataAccessObject mongoBiotaxonomicBlackListRepositoryFactory;
        private readonly IBlackListDataAccessObject xmlBiotaxonomicBlackListRepositoryFactory;
        private readonly ITaxonRankTypesDataAccessObject mongoTaxonRankTypesDataAccessObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="BiotaxonomyMongoDatabaseSeeder"/> class.
        /// </summary>
        /// <param name="mongoTaxonRanksDataAccessObject">MongoDB instance of <see cref="ITaxonRanksDataAccessObject"/>.</param>
        /// <param name="xmlTaxonRanksDataAccessObject">XML instance of <see cref="ITaxonRanksDataAccessObject"/>.</param>
        /// <param name="mongoBiotaxonomicBlackListRepositoryFactory">MongoDB instance of <see cref="IBlackListDataAccessObject"/>.</param>
        /// <param name="xmlBiotaxonomicBlackListRepositoryFactory">XML instance of <see cref="IBlackListDataAccessObject"/>.</param>
        /// <param name="mongoTaxonRankTypesDataAccessObject">Instance of <see cref="ITaxonRankTypesDataAccessObject"/>.</param>
        public BiotaxonomyMongoDatabaseSeeder(
            ITaxonRanksDataAccessObject mongoTaxonRanksDataAccessObject,
            ITaxonRanksDataAccessObject xmlTaxonRanksDataAccessObject,
            IBlackListDataAccessObject mongoBiotaxonomicBlackListRepositoryFactory,
            IBlackListDataAccessObject xmlBiotaxonomicBlackListRepositoryFactory,
            ITaxonRankTypesDataAccessObject mongoTaxonRankTypesDataAccessObject)
        {
            this.mongoTaxonRanksDataAccessObject = mongoTaxonRanksDataAccessObject ?? throw new ArgumentNullException(nameof(mongoTaxonRanksDataAccessObject));
            this.xmlTaxonRanksDataAccessObject = xmlTaxonRanksDataAccessObject ?? throw new ArgumentNullException(nameof(xmlTaxonRanksDataAccessObject));
            this.mongoBiotaxonomicBlackListRepositoryFactory = mongoBiotaxonomicBlackListRepositoryFactory ?? throw new ArgumentNullException(nameof(mongoBiotaxonomicBlackListRepositoryFactory));
            this.xmlBiotaxonomicBlackListRepositoryFactory = xmlBiotaxonomicBlackListRepositoryFactory ?? throw new ArgumentNullException(nameof(xmlBiotaxonomicBlackListRepositoryFactory));
            this.mongoTaxonRankTypesDataAccessObject = mongoTaxonRankTypesDataAccessObject ?? throw new ArgumentNullException(nameof(mongoTaxonRankTypesDataAccessObject));
        }

        /// <inheritdoc/>
        public async Task<object> SeedAsync()
        {
            await this.SeedTaxonRankTypeCollectionAsync().ConfigureAwait(false);
            await this.SeedTaxonRankCollectionAsync().ConfigureAwait(false);
            await this.SeedBlackListCollectionAsync().ConfigureAwait(false);

            return true;
        }

        private async Task SeedTaxonRankCollectionAsync()
        {
            var items = await this.xmlTaxonRanksDataAccessObject.GetAllAsync().ConfigureAwait(false);

            foreach (var item in items)
            {
                await this.mongoTaxonRanksDataAccessObject.UpsertAsync(item).ConfigureAwait(false);
            }
        }

        private async Task SeedTaxonRankTypeCollectionAsync()
        {
            await this.mongoTaxonRankTypesDataAccessObject.SeedFromTaxonRankTypeEnumAsync().ConfigureAwait(false);
        }

        private async Task SeedBlackListCollectionAsync()
        {
            var items = await this.xmlBiotaxonomicBlackListRepositoryFactory.GetAllAsync().ConfigureAwait(false);

            await this.mongoBiotaxonomicBlackListRepositoryFactory.InsertManyAsync(items).ConfigureAwait(false);
        }
    }
}
