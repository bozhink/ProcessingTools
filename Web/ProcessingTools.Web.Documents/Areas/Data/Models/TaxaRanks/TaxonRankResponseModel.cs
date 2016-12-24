namespace ProcessingTools.Web.Documents.Areas.Data.Models.TaxaRanks
{
    using Contracts;

    public class TaxonRankResponseModel : ITaxonRankResponseModel
    {
        public string Rank { get; set; }

        public string TaxonName { get; set; }
    }
}
