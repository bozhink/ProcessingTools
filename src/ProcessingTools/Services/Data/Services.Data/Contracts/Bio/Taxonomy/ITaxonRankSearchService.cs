namespace ProcessingTools.Services.Data.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services;

    public interface ITaxonRankSearchService : ISearchService<string, ITaxonRank>
    {
    }
}
