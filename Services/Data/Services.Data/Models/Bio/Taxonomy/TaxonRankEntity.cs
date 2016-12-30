namespace ProcessingTools.Services.Data.Models.Bio.Taxonomy
{
    using System.Collections.Generic;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;
    using ProcessingTools.Bio.Taxonomy.Types;

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
