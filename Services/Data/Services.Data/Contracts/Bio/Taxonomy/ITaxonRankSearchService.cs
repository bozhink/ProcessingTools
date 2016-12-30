namespace ProcessingTools.Services.Data.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Contracts.Services;

    public interface ITaxonRankSearchService : ISearchService<string, ITaxonRank>
    {
    }
}
