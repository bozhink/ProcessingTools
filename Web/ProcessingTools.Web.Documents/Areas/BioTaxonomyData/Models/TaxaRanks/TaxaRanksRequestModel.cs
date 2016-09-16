namespace ProcessingTools.Web.Documents.Areas.BioTaxonomyData.Models.TaxaRanks
{
    using System.Collections.Generic;

    public class TaxaRanksRequestModel
    {
        public ICollection<TaxonRankRequestModel> Taxa { get; set; } = new HashSet<TaxonRankRequestModel>();
    }
}
