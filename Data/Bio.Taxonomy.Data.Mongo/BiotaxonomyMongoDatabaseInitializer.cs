namespace ProcessingTools.Bio.Taxonomy.Data.Mongo
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Models;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo.Factories;

    public class BiotaxonomyMongoDatabaseInitializer : IBiotaxonomyMongoDatabaseInitializer
    {
        private readonly IMongoDatabase db;

        public BiotaxonomyMongoDatabaseInitializer(IBiotaxonomyMongoDatabaseProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.db = provider.Create();
        }

        public async Task Initialize()
        {
            await this.CreateIndicesToTaxonRankCollection();
            await this.CreateIndicesToTaxonRankTypesCollection();
            await this.CreateIndicesToBlackListCollection();
        }

        private async Task<object> CreateIndicesToTaxonRankCollection()
        {
            string collectionName = CollectionNameFactory.Create<MongoTaxonRankEntity>();

            var collection = this.db.GetCollection<MongoTaxonRankEntity>(collectionName);

            var result = await collection.Indexes
                .CreateOneAsync(
                    Builders<MongoTaxonRankEntity>.IndexKeys.Ascending(t => t.Name));

            return result;
        }

        private async Task<object> CreateIndicesToTaxonRankTypesCollection()
        {
            string collectionName = CollectionNameFactory.Create<MongoTaxonRankTypeEntity>();

            var collection = this.db.GetCollection<MongoTaxonRankTypeEntity>(collectionName);

            var indexOptions = new CreateIndexOptions
            {
                Unique = true,
                Sparse = false
            };

            var result = await collection.Indexes
                .CreateOneAsync(
                    Builders<MongoTaxonRankTypeEntity>.IndexKeys.Ascending(t => t.RankType),
                    indexOptions);

            result = await collection.Indexes
                .CreateOneAsync(
                    Builders<MongoTaxonRankTypeEntity>.IndexKeys.Ascending(t => t.Name),
                    indexOptions);

            return result;
        }

        private async Task<object> CreateIndicesToBlackListCollection()
        {
            string collectionName = CollectionNameFactory.Create<MongoBlackListEntity>();

            var collection = this.db.GetCollection<MongoBlackListEntity>(collectionName);

            var indexOptions = new CreateIndexOptions
            {
                Unique = true,
                Sparse = false
            };

            var result = await collection.Indexes
                .CreateOneAsync(
                    Builders<MongoBlackListEntity>.IndexKeys.Ascending(t => t.Content),
                    indexOptions);

            return result;
        }
    }
}
