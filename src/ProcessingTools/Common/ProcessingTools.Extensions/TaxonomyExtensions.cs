// <copyright file="TaxonomyExtensions.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;

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
    }
}
