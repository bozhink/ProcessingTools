namespace ProcessingTools.Bio.Taxonomy.Services.Data.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Types;

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
