namespace ProcessingTools.Bio.Taxonomy.Data.Seed
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.Contracts;

    public class BiotaxonomyMongoDatabaseSeeder : IBiotaxonomyMongoDatabaseSeeder
    {
        private readonly IMongoTaxonRankRepositoryProvider mongoTaxonRankRepositoryProvider;
        private readonly IXmlTaxonRankRepositoryProvider xmlTaxonRankRepositoryProvider;

        public BiotaxonomyMongoDatabaseSeeder(
            IMongoTaxonRankRepositoryProvider mongoTaxonRankRepositoryProvider,
            IXmlTaxonRankRepositoryProvider xmlTaxonRankRepositoryProvider)
        {
            if (mongoTaxonRankRepositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(mongoTaxonRankRepositoryProvider));
            }

            if (xmlTaxonRankRepositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(xmlTaxonRankRepositoryProvider));
            }

            this.mongoTaxonRankRepositoryProvider = mongoTaxonRankRepositoryProvider;
            this.xmlTaxonRankRepositoryProvider = xmlTaxonRankRepositoryProvider;
        }

        public async Task Seed()
        {
            await this.SeedTaxonRankRepository();
        }

        private async Task SeedTaxonRankRepository()
        {
            var mongoTaxonRankRepository = this.mongoTaxonRankRepositoryProvider.Create();
            var xmlTaxonRankRepository = this.xmlTaxonRankRepositoryProvider.Create();
            var entities = await xmlTaxonRankRepository.All();

            foreach (var entity in entities)
            {
                await mongoTaxonRankRepository.Add(entity);
            }
        }
    }
}
