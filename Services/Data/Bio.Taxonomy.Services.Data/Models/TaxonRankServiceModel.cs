namespace ProcessingTools.Bio.Taxonomy.Services.Data.Models
{
    using System;
    using ProcessingTools.Bio.Taxonomy.Contracts;

    public class TaxonRankServiceModel : ITaxonRank
    {
        public string Rank { get; set; }

        public string ScientificName { get; set; }
    }
}
