namespace ProcessingTools.Bio.Taxonomy.Services.Data.Models
{
    using ProcessingTools.Bio.Taxonomy.Contracts;

    public class TaxonRankWithWhiteListingServiceModel : TaxonRankServiceModel, ITaxonRankWithWhiteListing
    {
        public TaxonRankWithWhiteListingServiceModel()
        {
            this.IsWhiteListed = false;
        }

        public bool IsWhiteListed { get; set; }
    }
}