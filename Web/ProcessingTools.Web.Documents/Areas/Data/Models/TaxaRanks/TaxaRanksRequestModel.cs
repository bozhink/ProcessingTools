namespace ProcessingTools.Web.Documents.Areas.Data.Models.TaxaRanks
{
    using System.Collections.Generic;

    public class TaxaRanksRequestModel
    {
        public ICollection<TaxonRankRequestModel> Taxa { get; set; } = new HashSet<TaxonRankRequestModel>();
    }
}
