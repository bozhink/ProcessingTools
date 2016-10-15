namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    using ProcessingTools.Bio.Taxonomy.Types;

    internal class MinimalTaxonNamePart : IMinimalTaxonNamePart
    {
        public virtual string FullName { get; set; }

        public virtual SpeciesPartType Rank { get; set; }

        public override string ToString()
        {
            return this.FullName + " " + this.Rank;
        }
    }
}
