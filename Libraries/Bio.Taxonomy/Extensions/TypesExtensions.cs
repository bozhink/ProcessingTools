namespace ProcessingTools.Bio.Taxonomy.Extensions
{
    using System;
    using System.Linq;
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

        public static string MapTaxonTypeToTaxonTypeString(this TaxonType type)
        {
            return type.ToString().ToLower();
        }

        public static TaxonType MapTaxonTypeStringToTaxonType(this string type)
        {
            string typeLowerCase = type.ToLower();
            switch (typeLowerCase)
            {
                case XmlInternalSchemaConstants.AnyTaxonTypeValue:
                    return TaxonType.Any;

                case XmlInternalSchemaConstants.LowerTaxonTypeValue:
                    return TaxonType.Lower;

                case XmlInternalSchemaConstants.HigherTaxonTypeValue:
                    return TaxonType.Higher;

                default:
                    return TaxonType.Undefined;
            }
        }

        public static SpeciesPartType ToSpeciesPartType(this string type)
        {
            string typeLowerCase = type.ToLower();
            var result = Enum.GetValues(typeof(SpeciesPartType))
                .Cast<SpeciesPartType>()
                .Where(r => r.ToString().ToLower() == typeLowerCase)
                .FirstOrDefault();

            return result;
        }
    }
}
