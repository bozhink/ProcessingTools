namespace ProcessingTools.Bio.Taxonomy.Data.Seed
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using MongoDB.Driver;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Repositories.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.Contracts;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Data.Common.Mongo.Factories;
    using ProcessingTools.Extensions;

    public class BiotaxonomyMongoDatabaseSeeder : IBiotaxonomyMongoDatabaseSeeder
    {
        private readonly IBiotaxonomyMongoDatabaseProvider biotaxonomyMongoDatabaseProvider;
        private readonly IMongoTaxonRankRepositoryProvider mongoTaxonRankRepositoryProvider;
        private readonly ITaxonRankRepositoryProvider taxonRankRepositoryProvider;

        private readonly IMongoBiotaxonomicBlackListRepositoryProvider mongoBiotaxonomicBlackListRepositoryProvider;
        private readonly IBiotaxonomicBlackListIterableRepositoryProvider biotaxonomicBlackListIterableRepositoryProvider;

        public BiotaxonomyMongoDatabaseSeeder(
            IBiotaxonomyMongoDatabaseProvider biotaxonomyMongoDatabaseProvider,
            IMongoTaxonRankRepositoryProvider mongoTaxonRankRepositoryProvider,
            ITaxonRankRepositoryProvider taxonRankRepositoryProvider,
            IMongoBiotaxonomicBlackListRepositoryProvider mongoBiotaxonomicBlackListRepositoryProvider,
            IBiotaxonomicBlackListIterableRepositoryProvider biotaxonomicBlackListIterableRepositoryProvider)
        {
            if (biotaxonomyMongoDatabaseProvider == null)
            {
                throw new ArgumentNullException(nameof(biotaxonomyMongoDatabaseProvider));
            }

            if (mongoTaxonRankRepositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(mongoTaxonRankRepositoryProvider));
            }

            if (taxonRankRepositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(taxonRankRepositoryProvider));
            }

            if (mongoBiotaxonomicBlackListRepositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(mongoBiotaxonomicBlackListRepositoryProvider));
            }

            if (biotaxonomicBlackListIterableRepositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(biotaxonomicBlackListIterableRepositoryProvider));
            }

            this.biotaxonomyMongoDatabaseProvider = biotaxonomyMongoDatabaseProvider;
            this.mongoTaxonRankRepositoryProvider = mongoTaxonRankRepositoryProvider;
            this.taxonRankRepositoryProvider = taxonRankRepositoryProvider;
            this.mongoBiotaxonomicBlackListRepositoryProvider = mongoBiotaxonomicBlackListRepositoryProvider;
            this.biotaxonomicBlackListIterableRepositoryProvider = biotaxonomicBlackListIterableRepositoryProvider;
        }

        public async Task<object> Seed()
        {
            await this.SeedTaxonRankTypeCollection();
            await this.SeedTaxonRankCollection();
            await this.SeedBlackListCollection();

            return true;
        }

        private async Task SeedTaxonRankCollection()
        {
            var mongoTaxonRankRepository = this.mongoTaxonRankRepositoryProvider.Create();

            var repository = this.taxonRankRepositoryProvider.Create();
            var entities = await repository.All();

            foreach (var entity in entities)
            {
                await mongoTaxonRankRepository.Add(entity);
            }

            repository.TryDispose();
            mongoTaxonRankRepository.TryDispose();
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

        private async Task SeedBlackListCollection()
        {
            var mongoBiotaxonomicBlackListRepository = this.mongoBiotaxonomicBlackListRepositoryProvider.Create();
            var repository = this.biotaxonomicBlackListIterableRepositoryProvider.Create();

            var entities = repository.Entities;
            foreach (var entity in entities)
            {
                await mongoBiotaxonomicBlackListRepository.Add(entity);
            }

            repository.TryDispose();
            mongoBiotaxonomicBlackListRepository.TryDispose();
        }
    }
}
