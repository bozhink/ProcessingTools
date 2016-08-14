namespace ProcessingTools.Bio.Taxonomy.Data.Seed
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using MongoDB.Driver;

    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.Contracts;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Data.Common.Mongo.Factories;

    public class BiotaxonomyMongoDatabaseSeeder : IBiotaxonomyMongoDatabaseSeeder
    {
        private readonly IBiotaxonomyMongoDatabaseProvider biotaxonomyMongoDatabaseProvider;
        private readonly IMongoTaxonRankRepositoryProvider mongoTaxonRankRepositoryProvider;
        private readonly IXmlTaxonRankRepositoryProvider xmlTaxonRankRepositoryProvider;

        public BiotaxonomyMongoDatabaseSeeder(
            IBiotaxonomyMongoDatabaseProvider biotaxonomyMongoDatabaseProvider,
            IMongoTaxonRankRepositoryProvider mongoTaxonRankRepositoryProvider,
            IXmlTaxonRankRepositoryProvider xmlTaxonRankRepositoryProvider)
        {
            if (biotaxonomyMongoDatabaseProvider == null)
            {
                throw new ArgumentNullException(nameof(biotaxonomyMongoDatabaseProvider));
            }

            if (mongoTaxonRankRepositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(mongoTaxonRankRepositoryProvider));
            }

            if (xmlTaxonRankRepositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(xmlTaxonRankRepositoryProvider));
            }

            this.biotaxonomyMongoDatabaseProvider = biotaxonomyMongoDatabaseProvider;
            this.mongoTaxonRankRepositoryProvider = mongoTaxonRankRepositoryProvider;
            this.xmlTaxonRankRepositoryProvider = xmlTaxonRankRepositoryProvider;
        }

        public async Task Seed()
        {
            await this.SeedTaxonRankTypeCollection();
            await this.SeedTaxonRankCollection();
        }

        private async Task SeedTaxonRankCollection()
        {
            var mongoTaxonRankRepository = this.mongoTaxonRankRepositoryProvider.Create();
            var xmlTaxonRankRepository = this.xmlTaxonRankRepositoryProvider.Create();
            var entities = await xmlTaxonRankRepository.All();

            foreach (var entity in entities)
            {
                await mongoTaxonRankRepository.Add(entity);
            }
        }

        private async Task SeedTaxonRankTypeCollection()
        {
            var db = this.biotaxonomyMongoDatabaseProvider.Create();

            string collectionName = CollectionNameFactory.Create<MongoTaxonRankTypeEntity>();
            var collection = db.GetCollection<MongoTaxonRankTypeEntity>(collectionName);

            var entities = Enum.GetValues(typeof(TaxonRankType)).Cast<TaxonRankType>()
                .Select(rank => new MongoTaxonRankTypeEntity
                {
                    RankType = rank
                })
                .ToList();

            await collection.InsertManyAsync(
                entities,
                new InsertManyOptions
                {
                    IsOrdered = false,
                    BypassDocumentValidation = false
                });
        }
    }
}
