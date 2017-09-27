namespace ProcessingTools.Services.Data.Models.Bio.Taxonomy
{
    using System.Collections.Generic;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    internal class TaxonClassificationServiceModel : ITaxonClassification
    {
        public TaxonClassificationServiceModel()
        {
            this.Classification = new List<ITaxonRank>();
        }

        public string Authority { get; set; }

        public string CanonicalName { get; set; }

        public ICollection<ITaxonRank> Classification { get; private set; }

        public TaxonRankType Rank { get; set; }

        public string ScientificName { get; set; }
    }
}
