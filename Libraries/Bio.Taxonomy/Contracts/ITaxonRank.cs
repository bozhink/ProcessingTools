namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    using ProcessingTools.Enumerations;

    public interface ITaxonRank
    {
        string ScientificName { get; }

        TaxonRankType Rank { get; }
    }
}
