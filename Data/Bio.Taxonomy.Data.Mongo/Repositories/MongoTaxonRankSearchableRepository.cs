namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Repositories;

    public class MongoTaxonRankSearchableRepository : MongoSearchableRepository<MongoTaxonRankEntity, ITaxonRankEntity>, IMongoTaxonRankSearchableRepository
    {
        public MongoTaxonRankSearchableRepository(IBiotaxonomyMongoDatabaseProvider provider)
            : base(provider)
        {
        }
    }
}
