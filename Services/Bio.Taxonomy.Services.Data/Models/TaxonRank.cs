namespace ProcessingTools.Bio.Taxonomy.Services.Data.Models
{
    using Contracts;

    public class TaxonRankDataServiceResponseModel : ITaxonRank
    {
        public string ScientificName { get; set; }

        public string Rank { get; set; }
    }
}