namespace ProcessingTools.Bio.Taxonomy.Data.Seed.Seeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Bio.Taxonomy.Data.Seed.Contracts;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Data.Models.Mongo.Bio.Taxonomy;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Extensions;

    public class BiotaxonomyMongoDatabaseSeeder : IBiotaxonomyMongoDatabaseSeeder
    {
        private readonly IMongoDatabase db;

        private readonly ITaxonRanksDataAccessObject mongoTaxonRanksDataAccessObject;
        private readonly IRepositoryFactory<ITaxonRanksRepository> taxonRankRepositoryFactory;

        private readonly IBlackListDataAccessObject mongoBiotaxonomicBlackListRepositoryFactory;
        private readonly IRepositoryFactory<IBiotaxonomicBlackListRepository> biotaxonomicBlackListIterableRepositoryFactory;

        public BiotaxonomyMongoDatabaseSeeder(
            IMongoDatabaseProvider databaseProvider,
            ITaxonRanksDataAccessObject mongoTaxonRanksDataAccessObject,
            IRepositoryFactory<ITaxonRanksRepository> taxonRankRepositoryFactory,
            IBlackListDataAccessObject mongoBiotaxonomicBlackListRepositoryFactory,
            IRepositoryFactory<IBiotaxonomicBlackListRepository> biotaxonomicBlackListIterableRepositoryFactory)
        {
            if (databaseProvider == null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            this.db = databaseProvider.Create();
            this.mongoTaxonRanksDataAccessObject = mongoTaxonRanksDataAccessObject ?? throw new ArgumentNullException(nameof(mongoTaxonRanksDataAccessObject));
            this.taxonRankRepositoryFactory = taxonRankRepositoryFactory ?? throw new ArgumentNullException(nameof(taxonRankRepositoryFactory));
            this.mongoBiotaxonomicBlackListRepositoryFactory = mongoBiotaxonomicBlackListRepositoryFactory ?? throw new ArgumentNullException(nameof(mongoBiotaxonomicBlackListRepositoryFactory));
            this.biotaxonomicBlackListIterableRepositoryFactory = biotaxonomicBlackListIterableRepositoryFactory ?? throw new ArgumentNullException(nameof(biotaxonomicBlackListIterableRepositoryFactory));
        }

        public async Task<object> SeedAsync()
        {
            await this.SeedTaxonRankTypeCollectionAsync().ConfigureAwait(false);
            await this.SeedTaxonRankCollectionAsync().ConfigureAwait(false);
            await this.SeedBlackListCollectionAsync().ConfigureAwait(false);

            return true;
        }

        private async Task SeedTaxonRankCollectionAsync()
        {
            var repository = this.taxonRankRepositoryFactory.Create();
            var query = repository.Query;

            foreach (var entity in query)
            {
                await this.mongoTaxonRanksDataAccessObject.UpsertAsync(entity).ConfigureAwait(false);
            }

            repository.TryDispose();
        }

        private async Task SeedTaxonRankTypeCollectionAsync()
        {
            //// TODO string collectionName = MongoCollectionNameFactory.Create<TaxonRankTypeItem>();
            var collection = this.db.GetCollection<TaxonRankTypeItem>("taxonRankType");

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

            await collection.InsertManyAsync(entities, options).ConfigureAwait(false);
        }

        private async Task SeedBlackListCollectionAsync()
        {
            var repository = this.biotaxonomicBlackListIterableRepositoryFactory.Create();

            var entities = repository.Entities;
            foreach (var entity in entities)
            {
                await this.mongoBiotaxonomicBlackListRepositoryFactory.InsertOneAsync(entity.Content).ConfigureAwait(false);
            }

            repository.TryDispose();
        }
    }
}
