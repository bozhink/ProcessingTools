namespace ProcessingTools.Bio.Taxonomy.Data.Seed.Seeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Seed.Contracts;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Data.Common.Mongo.Factories;
    using ProcessingTools.Extensions;

    public class BiotaxonomyMongoDatabaseSeeder : IBiotaxonomyMongoDatabaseSeeder
    {
        private readonly IBiotaxonomyMongoDatabaseProvider biotaxonomyMongoDatabaseProvider;

        private readonly IRepositoryFactory<IMongoTaxonRankRepository> mongoTaxonRankRepositoryFactory;
        private readonly IRepositoryFactory<ITaxonRankRepository> taxonRankRepositoryFactory;

        private readonly IRepositoryFactory<IMongoBiotaxonomicBlackListRepository> mongoBiotaxonomicBlackListRepositoryFactory;
        private readonly IRepositoryFactory<IBiotaxonomicBlackListIterableRepository> biotaxonomicBlackListIterableRepositoryFactory;

        public BiotaxonomyMongoDatabaseSeeder(
            IBiotaxonomyMongoDatabaseProvider biotaxonomyMongoDatabaseProvider,
            IRepositoryFactory<IMongoTaxonRankRepository> mongoTaxonRankRepositoryFactory,
            IRepositoryFactory<ITaxonRankRepository> taxonRankRepositoryFactory,
            IRepositoryFactory<IMongoBiotaxonomicBlackListRepository> mongoBiotaxonomicBlackListRepositoryFactory,
            IRepositoryFactory<IBiotaxonomicBlackListIterableRepository> biotaxonomicBlackListIterableRepositoryFactory)
        {
            if (biotaxonomyMongoDatabaseProvider == null)
            {
                throw new ArgumentNullException(nameof(biotaxonomyMongoDatabaseProvider));
            }

            if (mongoTaxonRankRepositoryFactory == null)
            {
                throw new ArgumentNullException(nameof(mongoTaxonRankRepositoryFactory));
            }

            if (taxonRankRepositoryFactory == null)
            {
                throw new ArgumentNullException(nameof(taxonRankRepositoryFactory));
            }

            if (mongoBiotaxonomicBlackListRepositoryFactory == null)
            {
                throw new ArgumentNullException(nameof(mongoBiotaxonomicBlackListRepositoryFactory));
            }

            if (biotaxonomicBlackListIterableRepositoryFactory == null)
            {
                throw new ArgumentNullException(nameof(biotaxonomicBlackListIterableRepositoryFactory));
            }

            this.biotaxonomyMongoDatabaseProvider = biotaxonomyMongoDatabaseProvider;
            this.mongoTaxonRankRepositoryFactory = mongoTaxonRankRepositoryFactory;
            this.taxonRankRepositoryFactory = taxonRankRepositoryFactory;
            this.mongoBiotaxonomicBlackListRepositoryFactory = mongoBiotaxonomicBlackListRepositoryFactory;
            this.biotaxonomicBlackListIterableRepositoryFactory = biotaxonomicBlackListIterableRepositoryFactory;
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
            var mongoTaxonRankRepository = this.mongoTaxonRankRepositoryFactory.Create();

            var repository = this.taxonRankRepositoryFactory.Create();
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
            var mongoBiotaxonomicBlackListRepository = this.mongoBiotaxonomicBlackListRepositoryFactory.Create();
            var repository = this.biotaxonomicBlackListIterableRepositoryFactory.Create();

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
