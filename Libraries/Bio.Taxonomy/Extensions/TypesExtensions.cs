namespace ProcessingTools.Bio.Taxonomy.Extensions
{
    using Constants;
    using Types;

    public static class TypesExtensions
    {
        public static string MapTaxonRankTypeToTaxonRankString(this TaxonRankType rank)
        {
            switch (rank)
            {
                case TaxonRankType.AboveGenus:
                    return TaxaClassificationConstants.AboveGenusTaxonRankStringValue;

                case TaxonRankType.AboveFamily:
                    return TaxaClassificationConstants.AboveFamilyTaxonRankStringValue;

                default:
                    return rank.ToString().ToLower();
            }
        }
    }
}
