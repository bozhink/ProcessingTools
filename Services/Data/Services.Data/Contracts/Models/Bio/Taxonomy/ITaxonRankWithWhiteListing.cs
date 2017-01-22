namespace ProcessingTools.Services.Data.Contracts.Models.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    public interface ITaxonRankWithWhiteListing : ITaxonRank
    {
        bool IsWhiteListed { get; set; }
    }
}
