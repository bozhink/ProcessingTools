namespace ProcessingTools.Processors.Models.Bio.Taxonomy.Parsers
{
    using Contracts.Models.Bio.Taxonomy.Parsers;
    using ProcessingTools.Enumerations;

    internal class MinimalTaxonNamePart : IMinimalTaxonNamePart
    {
        private string fullName;
        private string name;
        private SpeciesPartType rank;
        private int contentHash;

        public virtual string FullName
        {
            get
            {
                return this.fullName;
            }

            set
            {
                this.fullName = value;
                this.UpdateContentHash();
            }
        }

        public virtual string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.UpdateContentHash();
            }
        }

        public virtual SpeciesPartType Rank
        {
            get
            {
                return this.rank;
            }

            set
            {
                this.rank = value;
                this.UpdateContentHash();
            }
        }

        public int ContentHash => this.contentHash;

        public override string ToString()
        {
            return string.Format(
                "{0} {1}",
                this.Rank.ToString().ToLower(),
                string.IsNullOrWhiteSpace(this.FullName) ? this.Name : this.FullName);
        }

        private void UpdateContentHash()
        {
            var contentString = this.FullName + this.Name + this.Rank;
            this.contentHash = contentString.GetHashCode();
        }
    }
}
