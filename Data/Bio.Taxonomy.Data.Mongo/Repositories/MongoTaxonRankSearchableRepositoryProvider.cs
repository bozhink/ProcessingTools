namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System;
    using Common.Models.Contracts;
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class MongoTaxonRankSearchableRepositoryProvider : IMongoTaxonRankSearchableRepositoryProvider
    {
        private readonly IBiotaxonomyMongoDatabaseProvider provider;

        public MongoTaxonRankSearchableRepositoryProvider(IBiotaxonomyMongoDatabaseProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        public ISearchableRepository<ITaxonRankEntity> Create()
        {
            return new MongoTaxonRankSearchableRepository(this.provider);
        }
    }
}
