namespace ProcessingTools.Bio.Taxonomy.Extensions
{
    using System;
    using Types;

    public static class StringExtensions
    {
        public static TaxonRankType MapTaxonRankStringToTaxonRankType(this string rank)
        {
            if (string.IsNullOrWhiteSpace(rank))
            {
                throw new ArgumentNullException(nameof(rank));
            }

            TaxonRankType rankType;
            if (Enum.TryParse(rank, true, out rankType))
            {
                return rankType;
            }
            else
            {
                return TaxonRankType.Other;
            }
        }
    }
}
