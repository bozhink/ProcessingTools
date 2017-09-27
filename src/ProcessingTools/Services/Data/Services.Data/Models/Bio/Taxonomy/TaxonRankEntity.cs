namespace ProcessingTools.Services.Data.Models.Bio.Taxonomy
{
    using System.Collections.Generic;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    internal class TaxonRankEntity : ITaxonRankEntity
    {
        public TaxonRankEntity()
        {
            this.Ranks = new HashSet<TaxonRankType>();
        }

        public bool IsWhiteListed { get; set; }

        public string Name { get; set; }

        public ICollection<TaxonRankType> Ranks { get; }
    }
}
