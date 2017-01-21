namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;
    using ProcessingTools.Enumerations;

    public class Taxon : ITaxonRankEntity
    {
        private ICollection<TaxonRankType> ranks;

        public Taxon()
        {
            this.ranks = new HashSet<TaxonRankType>();
            this.IsWhiteListed = false;
        }

        public string Name { get; set; }

        public bool IsWhiteListed { get; set; }

        public virtual ICollection<TaxonRankType> Ranks
        {
            get
            {
                return this.ranks;
            }

            set
            {
                this.ranks = value;
            }
        }
    }
}
