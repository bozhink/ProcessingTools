namespace ProcessingTools.Web.Documents.Areas.BioTaxonomyData.Models.BiotaxonomicBlackList
{
    using System.Collections.Generic;

    public class BlackListItemsRequestModel
    {
        public ICollection<BlackListItemRequestModel> Items { get; set; } = new HashSet<BlackListItemRequestModel>();
    }
}
