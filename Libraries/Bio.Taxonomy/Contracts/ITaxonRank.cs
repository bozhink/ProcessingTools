namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    using Types;

    public interface ITaxonRank
    {
        string ScientificName { get; }

        TaxonRankType Rank { get; }
    }
}