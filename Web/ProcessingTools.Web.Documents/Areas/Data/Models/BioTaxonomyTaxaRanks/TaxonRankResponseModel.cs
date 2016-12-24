namespace ProcessingTools.Web.Documents.Areas.Data.Models.BioTaxonomyTaxaRanks
{
    using Contracts;

    public class TaxonRankResponseModel : ITaxonRankResponseModel
    {
        public string Rank { get; set; }

        public string TaxonName { get; set; }
    }
}
