namespace ProcessingTools.Bio.Taxonomy.Services.Data.Models
{
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Types;

    public class TaxonRankServiceModel : ITaxonRank
    {
        public TaxonRankType Rank { get; set; }

        public string ScientificName { get; set; }
    }
}
