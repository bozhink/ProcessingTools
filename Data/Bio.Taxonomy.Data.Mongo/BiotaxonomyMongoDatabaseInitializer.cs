namespace ProcessingTools.Bio.Taxonomy.Data.Mongo
{
    using System;
    using System.Configuration;
    using System.Threading.Tasks;

    using Contracts;
    using Models;
    using MongoDB.Driver;
    using Repositories;

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
        }

        private async Task<object> CreateIndicesToTaxonRankCollection()
        {
            string collectionName = ConfigurationManager.AppSettings[MongoTaxonRankSearchableRepository.BiotaxonomyTaxaMongoCollectionNameKey];

            var collection = this.db.GetCollection<MongoTaxonRankEntity>(collectionName);

            var result = await collection.Indexes
                .CreateOneAsync(Builders<MongoTaxonRankEntity>.IndexKeys
                    .Ascending(t => t.Name));

            return result;
        }
    }
}
