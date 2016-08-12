namespace ProcessingTools.Bio.Taxonomy.Services.Data.Models.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Contracts;

    public interface ITaxonRankWithWhiteListing : ITaxonRank
    {
        bool IsWhiteListed { get; set; }
    }
}
