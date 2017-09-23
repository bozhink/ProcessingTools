namespace ProcessingTools.Services.Data.Models.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Enumerations;

    public class TaxonRankServiceModel : ITaxonRank
    {
        public TaxonRankType Rank { get; set; }

        public string ScientificName { get; set; }
    }
}
