namespace ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories
{
    using Models.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface ITaxonRankRepository : ITaxonRankSearchableRepository, ISearchableCountableCrudRepository<ITaxonRankEntity>
    {
    }
}
