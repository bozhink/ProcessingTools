namespace ProcessingTools.Web.Documents.Areas.Data.Models.BioTaxonomyBlackList
{
    using System.Collections.Generic;

    public class BlackListItemsRequestModel
    {
        public ICollection<BlackListItemRequestModel> Items { get; set; } = new HashSet<BlackListItemRequestModel>();
    }
}
