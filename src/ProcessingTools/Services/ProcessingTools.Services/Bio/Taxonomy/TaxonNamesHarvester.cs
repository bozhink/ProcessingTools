// <copyright file="TaxonNamesHarvester.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common.Code.Extensions;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Text;

    /// <summary>
    /// Taxon names harvester.
    /// </summary>
    public class TaxonNamesHarvester : ITaxonNamesHarvester
    {
        /// <inheritdoc/>
        public Task<IList<string>> HarvestAsync(XmlNode context) => Task.Run(() => this.ExtractTaxa(context, TaxonType.Any));

        /// <inheritdoc/>
        public Task<IList<string>> HarvestHigherTaxaAsync(XmlNode context) => Task.Run(() => this.ExtractTaxa(context, TaxonType.Higher));

        /// <inheritdoc/>
        public Task<IList<string>> HarvestLowerTaxaAsync(XmlNode context) => Task.Run(() => this.ExtractTaxa(context, TaxonType.Lower));

        private static void ReplaceXmlNodeInnerTextByItsFullNameAttribute(XmlNode node)
        {
            foreach (XmlNode fullNamedElement in node.SelectNodes(XPathStrings.ElementWithFullNameAttribute))
            {
                fullNamedElement.InnerText = fullNamedElement.Attributes[AttributeNames.FullName].InnerText;
                fullNamedElement.Attributes.RemoveNamedItem(AttributeNames.FullName);
            }
        }

        private IList<string> ExtractTaxa(XmlNode context, TaxonType type)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string typeString = type.ToString().ToLowerInvariant();
            string xpath = string.Empty;
            switch (type)
            {
                case TaxonType.Lower:
                case TaxonType.Higher:
                    xpath = string.Format(CultureInfo.InvariantCulture, "//tn[@type='{0}']|//tp:taxon-name[@type='{0}']", typeString);
                    break;

                case TaxonType.Any:
                    xpath = "//tn|//tp:taxon-name";
                    break;

                default:
                    throw new NotSupportedException();
            }

            var result = new List<string>();
            if (!string.IsNullOrWhiteSpace(xpath))
            {
                result = context.SelectNodesWithTaxPubXmlNamespaceManager(xpath)
                    .Cast<XmlNode>()
                    .Select(this.TaxonNameXmlNodeToString)
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToList();
            }

            return new HashSet<string>(result).ToArray();
        }

        private string TaxonNameXmlNodeToString(XmlNode node)
        {
            if (node is null)
            {
                return null;
            }

            XmlNode result = node.CloneNode(true).RemoveXmlNodes(".//object-id|.//tn-part[@type='uncertainty-rank']");
            if (result is null)
            {
                return null;
            }

            result.Attributes.RemoveAll();

            ReplaceXmlNodeInnerTextByItsFullNameAttribute(result);

            string innerXml = result.InnerXml
                .Replace("?", string.Empty, StringComparison.InvariantCulture)
                .RegexReplace(@"</[^>]*>(?=[^\s\)\]])(?!\Z)", " ")
                .RegexReplace(@"<[^>]+>", string.Empty)
                .RegexReplace(@"\s+", " ")
                .Trim();

            // Make single word-upper-case names in title case.
            if (Regex.IsMatch(innerXml, @"\A[A-Z]+\Z"))
            {
                innerXml = innerXml.ToFirstLetterUpperCase();
            }

            result.InnerXml = innerXml;

            return result.InnerText;
        }
    }
}
