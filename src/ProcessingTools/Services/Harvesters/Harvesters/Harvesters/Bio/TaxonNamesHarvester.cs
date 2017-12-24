namespace ProcessingTools.Harvesters.Harvesters.Bio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts.Harvesters.Bio;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;

    public class TaxonNamesHarvester : ITaxonNamesHarvester
    {
        public Task<string[]> HarvestAsync(XmlNode context) => Task.Run(() => this.ExtractTaxa(context, TaxonType.Any));

        public Task<string[]> HarvestHigherTaxaAsync(XmlNode context) => Task.Run(() => this.ExtractTaxa(context, TaxonType.Higher));

        public Task<string[]> HarvestLowerTaxaAsync(XmlNode context) => Task.Run(() => this.ExtractTaxa(context, TaxonType.Lower));

        private string[] ExtractTaxa(XmlNode context, TaxonType type)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string typeString = type.ToString().ToLowerInvariant();
            string xpath = string.Empty;
            switch (type)
            {
                case TaxonType.Lower:
                case TaxonType.Higher:
                    xpath = string.Format("//tn[@type='{0}']|//tp:taxon-name[@type='{0}']", typeString);
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
            if (node == null)
            {
                return null;
            }

            XmlNode result = node.CloneNode(true).RemoveXmlNodes(".//object-id|.//tn-part[@type='uncertainty-rank']");
            if (result == null)
            {
                return null;
            }

            result.Attributes.RemoveAll();
            this.ReplaceXmlNodeInnerTextByItsFullNameAttribute(result);

            string innerXml = result.InnerXml
                .Replace("?", string.Empty)
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

        private void ReplaceXmlNodeInnerTextByItsFullNameAttribute(XmlNode node)
        {
            foreach (XmlNode fullNamedElement in node.SelectNodes(XPathStrings.ElementWithFullNameAttribute))
            {
                fullNamedElement.InnerText = fullNamedElement.Attributes[AttributeNames.FullName].InnerText;
                fullNamedElement.Attributes.RemoveNamedItem(AttributeNames.FullName);
            }
        }
    }
}
