namespace ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories
{
    using Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface ITaxonRankRepository : IFirstFilterableRepository<ITaxonRankEntity>, IAsyncRepository<ITaxonRankEntity>, IUpdatableRepository<ITaxonRankEntity>, IQueryableRepository<ITaxonRankEntity>
    {
    }
}
