namespace ProcessingTools.Web.Documents.Areas.Data.Models.BioTaxonomyBlackList
{
    using System;
    using Contracts;

    public class SearchResposeModel
    {
        private readonly IBlackListItemResponseModel[] items;

        public SearchResposeModel(params IBlackListItemResponseModel[] items)
        {
            if (items == null || items.Length < 1)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.items = items;
        }

        public IBlackListItemResponseModel[] Items => this.items;
    }
}
