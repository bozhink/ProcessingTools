namespace ProcessingTools.Web.Documents.Areas.BioTaxonomyData.ViewModels.TaxaRanks
{
    using System.Collections.Generic;

    public class TaxaRanksViewModel
    {
        public ICollection<TaxonRankViewModel> Taxa { get; set; } = new HashSet<TaxonRankViewModel>();
    }
}
