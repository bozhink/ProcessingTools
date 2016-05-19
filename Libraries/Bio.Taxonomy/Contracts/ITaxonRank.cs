namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    public interface ITaxonRank
    {
        string ScientificName { get; set; }

        string Rank { get; set; }
    }
}