namespace ProcessingTools.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;

    public static class TaxonomyExtensions
    {
        public static XmlElement CreateTaxonNameXmlElement(this IDocument document, TaxonType type)
        {
            XmlElement tn = document.XmlDocument.CreateElement(ElementNames.TaxonName);
            tn.SetAttribute(AttributeNames.Type, type.ToString().ToLowerInvariant());
            return tn;
        }

        public static IEnumerable<string> ExtractUniqueNonParsedHigherTaxa(this XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var taxaNames = node.SelectNodes(".//tn[@type='higher'][not(tn-part)]")
                .Cast<XmlNode>()
                .Select(c => c.InnerText);

            var result = new HashSet<string>(taxaNames);

            return result;
        }

        public static TaxonRankType MapTaxonRankStringToTaxonRankType(this string rank)
        {
            TaxonRankType rankType = TaxonRankType.Other;

            if (!string.IsNullOrWhiteSpace(rank))
            {
                switch (rank.ToLowerInvariant())
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

        public static string MapTaxonRankTypeToTaxonRankString(this TaxonRankType rank)
        {
            switch (rank)
            {
                case TaxonRankType.AboveGenus:
                    return TaxaClassificationConstants.AboveGenusTaxonRankStringValue;

                case TaxonRankType.AboveFamily:
                    return TaxaClassificationConstants.AboveFamilyTaxonRankStringValue;

                default:
                    return rank.ToString().ToLowerInvariant();
            }
        }

        public static string MapTaxonTypeToTaxonTypeString(this TaxonType type)
        {
            return type.ToString().ToLowerInvariant();
        }

        public static string ToRankString(this SpeciesPartType type)
        {
            return type.ToString().ToLowerInvariant();
        }
    }
}
