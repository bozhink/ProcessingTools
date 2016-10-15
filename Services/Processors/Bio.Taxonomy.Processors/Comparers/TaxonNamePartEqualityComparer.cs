namespace ProcessingTools.Bio.Taxonomy.Processors.Comparers
{
    using System.Collections.Generic;
    using Models.Parsers;

    internal class TaxonNamePartEqualityComparer : IEqualityComparer<TaxonNamePart>
    {
        public bool Equals(TaxonNamePart x, TaxonNamePart y)
        {
            return ((x.FullName == y.FullName) && (x.Rank == y.Rank)) || (x.Name == y.Name);
        }

        public int GetHashCode(TaxonNamePart obj)
        {
            return obj.GetHashCode();
        }
    }
}
