namespace ProcessingTools.Bio.Taxonomy.Services.Data.Models
{
    using Contracts;

    public class TaxonRank : ITaxonRank
    {
        public string ScientificName { get; set; }

        public string Rank { get; set; }
    }
}