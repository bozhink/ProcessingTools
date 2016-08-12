namespace ProcessingTools.Bio.Taxonomy.Data.Common.Repositories.Contracts
{
    using Models.Contracts;
    using ProcessingTools.Data.Common.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface ITaxonRankRepositoryProvider : IGenericRepositoryProvider<IGenericRepository<ITaxonRankEntity>, ITaxonRankEntity>
    {
    }
}
