// <copyright file="TaxonomyExtensions.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Common
{
    using System;
    using System.Linq;

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
            if (Enum.TryParse(value: $"{type}", ignoreCase: true, result: out TaxonType taxonType))
            {
                return taxonType;
            }
            else
            {
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
            if (Enum.TryParse(value: $"{type}", ignoreCase: true, result: out SpeciesPartType speciesPartType))
            {
                return speciesPartType;
            }
            else
            {
                return SpeciesPartType.Undefined;
            }
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
