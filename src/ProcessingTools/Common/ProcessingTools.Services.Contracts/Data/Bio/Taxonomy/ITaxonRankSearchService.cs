namespace ProcessingTools.Services.Contracts.Data.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public interface ITaxonRankSearchService : ISearchService<string, ITaxonRank>
    {
    }
}
