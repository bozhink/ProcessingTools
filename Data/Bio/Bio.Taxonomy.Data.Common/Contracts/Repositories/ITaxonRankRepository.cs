namespace ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories
{
    using Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface ITaxonRankRepository : IFilterableRepository<ITaxonRankEntity>, IAsyncRepository<ITaxonRankEntity>, IUpdatableRepository<ITaxonRankEntity>, IQueryableRepository<ITaxonRankEntity>
    {
    }
}
