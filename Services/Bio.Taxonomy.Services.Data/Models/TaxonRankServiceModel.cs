namespace ProcessingTools.Bio.Taxonomy.Services.Data.Models
{
    using ProcessingTools.Bio.Taxonomy.Contracts;

    public class TaxonRankServiceModel : ITaxonRank
    {
        public TaxonRankServiceModel()
        {
            this.IsWhiteListed = false;
        }

        public string ScientificName { get; set; }

        public string Rank { get; set; }

        public bool IsWhiteListed { get; set; }
    }
}