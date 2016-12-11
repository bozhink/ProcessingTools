namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts.Repositories
{
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;

    public interface IMongoTaxonRankRepository : IMongoTaxonRankSearchableRepository, ITaxonRankRepository
    {
    }
}