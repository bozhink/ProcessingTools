namespace ProcessingTools.Web.Documents.Areas.BioTaxonomyData.ViewModels.BiotaxonomicBlackList
{
    using System.Collections.Generic;

    public class BlackListItemsViewModel
    {
        public ICollection<BlackListItemViewModel> Items { get; set; } = new HashSet<BlackListItemViewModel>();
    }
}
