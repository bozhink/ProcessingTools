namespace ProcessingTools.Bio.Taxonomy.Services.Data.Models
{
    using Taxonomy.Contracts;

    internal class TaxonRankServiceModel : ITaxonRank
    {
        public string ScientificName { get; set; }

        public string Rank { get; set; }
    }
}