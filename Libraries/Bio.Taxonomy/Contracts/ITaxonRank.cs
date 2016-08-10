namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    public interface ITaxonRank
    {
        string ScientificName { get; }

        string Rank { get; }
    }
}