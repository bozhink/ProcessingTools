namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Models
{
    using System.Collections.Generic;

    public class Taxon
    {
        private ICollection<string> ranks;

        public Taxon()
        {
            this.ranks = new HashSet<string>();
            this.IsWhiteListed = false;
        }

        public string Name { get; set; }

        public bool IsWhiteListed { get; set; }

        public virtual ICollection<string> Ranks
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
