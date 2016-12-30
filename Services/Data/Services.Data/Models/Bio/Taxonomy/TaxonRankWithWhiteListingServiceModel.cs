namespace ProcessingTools.Services.Data.Models.Bio.Taxonomy
{
    using Contracts.Models.Bio.Taxonomy;

    public class TaxonRankWithWhiteListingServiceModel : TaxonRankServiceModel, ITaxonRankWithWhiteListing
    {
        public TaxonRankWithWhiteListingServiceModel()
        {
            this.IsWhiteListed = false;
        }

        public bool IsWhiteListed { get; set; }
    }
}
