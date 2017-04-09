namespace ProcessingTools.Services.Data.Models.Bio.Taxonomy
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Models;
    using ProcessingTools.Enumerations;

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
