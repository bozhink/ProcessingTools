namespace ProcessingTools.Web.Documents.Areas.BioTaxonomyData.Models.TaxaRanks
{
    using Contracts;

    public class TaxonRankResponseModel : ITaxonRankResponseModel
    {
        public string Rank { get; set; }

        public string TaxonName { get; set; }
    }
}
