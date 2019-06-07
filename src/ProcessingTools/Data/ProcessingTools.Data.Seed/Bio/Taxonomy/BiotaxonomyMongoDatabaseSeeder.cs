namespace ProcessingTools.Data.Seed.Bio.Taxonomy
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Bio.Taxonomy;

    public class BiotaxonomyMongoDatabaseSeeder : IBiotaxonomyMongoDatabaseSeeder
    {
        private readonly ITaxonRanksDataAccessObject mongoTaxonRanksDataAccessObject;
        private readonly ITaxonRanksDataAccessObject xmlTaxonRanksDataAccessObject;
        private readonly IBlackListDataAccessObject mongoBiotaxonomicBlackListRepositoryFactory;
        private readonly IBlackListDataAccessObject xmlBiotaxonomicBlackListRepositoryFactory;
        private readonly ITaxonRankTypesDataAccessObject mongoTaxonRankTypesDataAccessObject;

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
