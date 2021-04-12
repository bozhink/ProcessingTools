// <copyright file="TaxonomyExtensions.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using ProcessingTools.Bio.Taxonomy.Common;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Taxonomy extensions.
    /// </summary>
    public static class TaxonomyExtensions
    {
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
            if (node is null)
            {
                return Array.Empty<string>();
            }

            var taxaNames = node.SelectNodes(".//tn[@type='higher'][not(tn-part)]")
                .Cast<XmlNode>()
                .Select(c => c.InnerText);

            var result = new HashSet<string>(taxaNames);

            return result;
        }
    }
}
