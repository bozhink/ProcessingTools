// <copyright file="TaxonomyExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Taxonomy extensions.
    /// </summary>
    public static class TaxonomyExtensions
    {
        /// <summary>
        /// Maps taxon type string to <see cref="TaxonType"/>.
        /// </summary>
        /// <param name="type">String value of the taxon type.</param>
        /// <returns>Taxon type as <see cref="TaxonType"/> result.</returns>
        public static TaxonType MapTaxonTypeStringToTaxonType(this string type)
        {
            string typeLowerCase = type.ToLowerInvariant();
            switch (typeLowerCase)
            {
                case AttributeValues.TaxonTypeAny:
                    return TaxonType.Any;

                case AttributeValues.TaxonTypeLower:
                    return TaxonType.Lower;

                case AttributeValues.TaxonTypeHigher:
                    return TaxonType.Higher;

                default:
                    return TaxonType.Undefined;
            }
        }

        /// <summary>
        /// Maps species part type as string to <see cref="SpeciesPartType"/>.
        /// </summary>
        /// <param name="type">String value of the species part type.</param>
        /// <returns>Species part type as <see cref="SpeciesPartType"/> result.</returns>
        public static SpeciesPartType ToSpeciesPartType(this string type)
        {
            string typeLowerCase = type.ToUpperInvariant();
            var result = Enum.GetValues(typeof(SpeciesPartType))
                .Cast<SpeciesPartType>()
                .FirstOrDefault(r => r.ToString().ToUpperInvariant() == typeLowerCase);

            return result;
        }

        /// <summary>
        /// Create taxon name <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="document"><see cref="IDocument"/> parent.</param>
        /// <param name="type">Type of the taxon.</param>
        /// <returns><see cref="XmlElement"/> for the document.</returns>
        public static XmlElement CreateTaxonNameXmlElement(this IDocument document, TaxonType type)
        {
            XmlElement tn = document.XmlDocument.CreateElement(ElementNames.TaxonName);
            tn.SetAttribute(AttributeNames.Type, type.ToString().ToLowerInvariant());
            return tn;
        }

        /// <summary>
        /// Extract unique non parsed higher taxa.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> to be harvested.</param>
        /// <returns>Taxon names.</returns>
        public static IEnumerable<string> ExtractUniqueNonParsedHigherTaxa(this XmlNode node)
        {
            if (node == null)
            {
                return Array.Empty<string>();
            }

            var taxaNames = node.SelectNodes(".//tn[@type='higher'][not(tn-part)]")
                .Cast<XmlNode>()
                .Select(c => c.InnerText);

            var result = new HashSet<string>(taxaNames);

            return result;
        }

        /// <summary>
        /// Map taxon rank string to <see cref="TaxonRankType"/>.
        /// </summary>
        /// <param name="rank">Taxon rank as string.</param>
        /// <returns>Corresponding <see cref="TaxonRankType"/> value.</returns>
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

        /// <summary>
        /// Map <see cref="TaxonRankType"/> to taxon rank string.
        /// </summary>
        /// <param name="rank"><see cref="TaxonRankType"/> value.</param>
        /// <returns>String value for the rank.</returns>
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

        /// <summary>
        /// Map <see cref="TaxonType"/> to taxon type string.
        /// </summary>
        /// <param name="type"><see cref="TaxonType"/> value.</param>
        /// <returns>String value for the taxon type.</returns>
        public static string MapTaxonTypeToTaxonTypeString(this TaxonType type)
        {
            return type.ToString().ToLowerInvariant();
        }

        /// <summary>
        /// Map <see cref="SpeciesPartType"/> to rank string.
        /// </summary>
        /// <param name="type"><see cref="SpeciesPartType"/> value.</param>
        /// <returns>Rank string value of the type.</returns>
        public static string ToRankString(this SpeciesPartType type)
        {
            return type.ToString().ToLowerInvariant();
        }
    }
}
