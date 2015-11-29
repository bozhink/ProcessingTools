namespace ProcessingTools.Bio.Taxonomy.Services.Data.Contracts
{
    public interface ITaxonRank
    {
        string ScientificName { get; set; }

        string Rank { get; set; }
    }
}