namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;

    public class MongoTaxonRankRepositoryProvider : IMongoTaxonRankRepositoryProvider
    {
        private readonly IBiotaxonomyMongoDatabaseProvider provider;

        public MongoTaxonRankRepositoryProvider(IBiotaxonomyMongoDatabaseProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        public ITaxonRankRepository Create()
        {
            return new MongoTaxonRankRepository(this.provider);
        }
    }
}
