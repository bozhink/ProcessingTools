namespace ProcessingTools.Bio.Taxonomy.Data.Mongo
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Models;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;

    public class BiotaxonomyMongoDatabaseInitializer : IBiotaxonomyMongoDatabaseInitializer
    {
        private readonly IMongoDatabase db;

        public BiotaxonomyMongoDatabaseInitializer(IMongoDatabaseProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.db = provider.Create();
        }

        public async Task<object> InitializeAsync()
        {
            await this.CreateIndicesToTaxonRankCollection().ConfigureAwait(false);
            await this.CreateIndicesToTaxonRankTypesCollection().ConfigureAwait(false);
            await this.CreateIndicesToBlackListCollection().ConfigureAwait(false);

            return true;
        }

        private async Task<object> CreateIndicesToTaxonRankCollection()
        {
            string collectionName = MongoCollectionNameFactory.Create<MongoTaxonRankEntity>();

            var collection = this.db.GetCollection<MongoTaxonRankEntity>(collectionName);

            var result = await collection.Indexes
                .CreateOneAsync(
                    Builders<MongoTaxonRankEntity>.IndexKeys.Ascending(t => t.Name))
                .ConfigureAwait(false);

            return result;
        }

        private async Task<object> CreateIndicesToTaxonRankTypesCollection()
        {
            string collectionName = MongoCollectionNameFactory.Create<MongoTaxonRankTypeEntity>();

            var collection = this.db.GetCollection<MongoTaxonRankTypeEntity>(collectionName);

            var indexOptions = new CreateIndexOptions
            {
                Unique = true,
                Sparse = false
            };

            await collection.Indexes
                .CreateOneAsync(
                    Builders<MongoTaxonRankTypeEntity>.IndexKeys.Ascending(t => t.RankType),
                    indexOptions)
                .ConfigureAwait(false);

            var result = await collection.Indexes
                .CreateOneAsync(
                    Builders<MongoTaxonRankTypeEntity>.IndexKeys.Ascending(t => t.Name),
                    indexOptions)
                .ConfigureAwait(false);

            return result;
        }

        private async Task<object> CreateIndicesToBlackListCollection()
        {
            string collectionName = MongoCollectionNameFactory.Create<MongoBlackListEntity>();

            var collection = this.db.GetCollection<MongoBlackListEntity>(collectionName);

            var indexOptions = new CreateIndexOptions
            {
                Unique = true,
                Sparse = false
            };

            var result = await collection.Indexes
                .CreateOneAsync(
                    Builders<MongoBlackListEntity>.IndexKeys.Ascending(t => t.Content),
                    indexOptions)
                .ConfigureAwait(false);

            return result;
        }
    }
}
