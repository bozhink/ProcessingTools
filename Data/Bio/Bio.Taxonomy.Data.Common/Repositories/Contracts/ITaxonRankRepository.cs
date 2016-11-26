namespace ProcessingTools.Bio.Taxonomy.Data.Common.Repositories.Contracts
{
    using Models.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface ITaxonRankRepository : ITaxonRankSearchableRepository, ISearchableCountableCrudRepository<ITaxonRankEntity>
    {
    }
}
