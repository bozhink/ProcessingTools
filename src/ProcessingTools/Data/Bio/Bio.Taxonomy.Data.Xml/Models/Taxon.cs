namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

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
