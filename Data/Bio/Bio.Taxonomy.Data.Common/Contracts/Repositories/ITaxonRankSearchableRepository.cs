namespace ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories
{
    using Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface ITaxonRankSearchableRepository : IRepository<ITaxonRankEntity>, IFiltrableRepository<ITaxonRankEntity>
    {
    }
}
