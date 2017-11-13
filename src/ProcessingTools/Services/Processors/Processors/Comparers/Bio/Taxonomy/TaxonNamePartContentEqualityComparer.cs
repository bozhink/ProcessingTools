namespace ProcessingTools.Processors.Comparers.Bio.Taxonomy
{
    using System.Collections.Generic;
    using ProcessingTools.Processors.Models.Contracts.Bio.Taxonomy.Parsers;

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
