namespace ProcessingTools.Bio.Taxonomy.Processors.Comparers
{
    using System.Collections.Generic;
    using Models.Parsers;

    internal class TaxonNamePartContentEqualityComparer : IEqualityComparer<ITaxonNamePart>
    {
        public bool Equals(ITaxonNamePart x, ITaxonNamePart y)
        {
            return x.ContentHash == y.ContentHash;
        }

        public int GetHashCode(ITaxonNamePart obj)
        {
            return obj.ContentHash;
        }
    }
}
