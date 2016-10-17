namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    using ProcessingTools.Bio.Taxonomy.Types;

    internal class MinimalTaxonNamePart : IMinimalTaxonNamePart
    {
        public virtual string FullName { get; set; }

        public virtual string Name { get; set; }

        public virtual SpeciesPartType Rank { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0} {1}",
                this.Rank.ToString().ToLower(),
                string.IsNullOrWhiteSpace(this.FullName) ? this.Name : this.FullName);
        }
    }
}
