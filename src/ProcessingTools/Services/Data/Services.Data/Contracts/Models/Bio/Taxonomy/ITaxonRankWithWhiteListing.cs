namespace ProcessingTools.Services.Data.Contracts.Models.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public interface ITaxonRankWithWhiteListing : ITaxonRank
    {
        bool IsWhiteListed { get; set; }
    }
}
