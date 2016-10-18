namespace ProcessingTools.Bio.Taxonomy.Processors.Comparers
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.Parsers;

    internal class TaxonNameContentEqualityComparer : IEqualityComparer<ITaxonName>
    {
        public bool Equals(ITaxonName x, ITaxonName y)
        {
            return this.GetHashCode(x) == this.GetHashCode(y);
        }

        public int GetHashCode(ITaxonName obj)
        {
            return string.Join(".", obj.Parts.Select(p => p.ContentHash)).GetHashCode();
        }
    }
}
