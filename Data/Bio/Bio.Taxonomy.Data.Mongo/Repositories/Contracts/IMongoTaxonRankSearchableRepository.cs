namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Data.Common.Mongo.Repositories.Contracts;

    public interface IMongoTaxonRankSearchableRepository : IMongoSearchableRepository<ITaxonRankEntity>, ITaxonRankSearchableRepository
    {
    }
}
