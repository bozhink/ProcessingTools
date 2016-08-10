namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    public interface ITaxonRankWithWhiteListing : ITaxonRank
    {
        bool IsWhiteListed { get; set; }
    }
}
