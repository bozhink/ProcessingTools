namespace ProcessingTools.Bio.Taxonomy.Extensions
{
    using System;
    using Constants;
    using ProcessingTools.Enumerations;

    public static class StringExtensions
    {
        public static TaxonRankType MapTaxonRankStringToTaxonRankType(this string rank)
        {
            TaxonRankType rankType = TaxonRankType.Other;

            if (!string.IsNullOrWhiteSpace(rank))
            {
                switch (rank.ToLower())
                {
                    case TaxaClassificationConstants.AboveGenusTaxonRankStringValue:
                        rankType = TaxonRankType.AboveGenus;
                        break;

                    case TaxaClassificationConstants.AboveFamilyTaxonRankStringValue:
                        rankType = TaxonRankType.AboveFamily;
                        break;

                    default:
                        Enum.TryParse(rank, true, out rankType);
                        break;
                }
            }

            return rankType;
        }
    }
}
