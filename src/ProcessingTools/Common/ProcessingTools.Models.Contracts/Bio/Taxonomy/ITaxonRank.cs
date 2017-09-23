namespace ProcessingTools.Models.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Enumerations;

    public interface ITaxonRank
    {
        string ScientificName { get; }

        TaxonRankType Rank { get; }
    }
}
