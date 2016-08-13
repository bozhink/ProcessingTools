namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

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

        public IGenericRepository<ITaxonRankEntity> Create()
        {
            return new MongoTaxonRankRepository(this.provider);
        }
    }
}
