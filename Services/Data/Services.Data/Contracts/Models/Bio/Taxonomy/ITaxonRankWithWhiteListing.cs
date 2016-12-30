namespace ProcessingTools.Services.Data.Contracts.Models.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.Contracts;

    public interface ITaxonRankWithWhiteListing : ITaxonRank
    {
        bool IsWhiteListed { get; set; }
    }
}
