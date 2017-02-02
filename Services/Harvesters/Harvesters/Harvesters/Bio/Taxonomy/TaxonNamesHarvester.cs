namespace ProcessingTools.Harvesters.Harvesters.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Harvesters.Bio.Taxonomy;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.DocumentProvider.Extensions;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Strings.Extensions;
    using ProcessingTools.Xml.Extensions;

    public class TaxonNamesHarvester : ITaxonNamesHarvester
    {
        public Task<IEnumerable<string>> Harvest(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.FromResult(this.ExtractTaxa(context, TaxonType.Any));
        }

        public Task<IEnumerable<string>> HarvestHigherTaxa(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.FromResult(this.ExtractTaxa(context, TaxonType.Higher));
        }

        public Task<IEnumerable<string>> HarvestLowerTaxa(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.FromResult(this.ExtractTaxa(context, TaxonType.Lower));
        }

        private IEnumerable<string> ExtractTaxa(XmlNode context, TaxonType type = TaxonType.Any)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string typeString = type.ToString().ToLower();
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
            }

            var result = new List<string>();
            if (!string.IsNullOrWhiteSpace(xpath))
            {
                result = context.SelectNodesWithTaxPubXmlNamespaceManager(xpath)
                    .Cast<XmlNode>()
                    .Select(this.TaxonNameXmlNodeToString)
                    .Distinct()
                    .ToList();
            }

            return new HashSet<string>(result);
        }

        private string TaxonNameXmlNodeToString(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            XmlNode result = node.CloneNode(true).RemoveXmlNodes(".//object-id|.//tn-part[@type='uncertainty-rank']");

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
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            foreach (XmlNode fullNamedElement in node.SelectNodes(XPathStrings.ElementWithFullNameAttribute))
            {
                fullNamedElement.InnerText = fullNamedElement.Attributes[AttributeNames.FullName].InnerText;
                fullNamedElement.Attributes.RemoveNamedItem(AttributeNames.FullName);
            }
        }
    }
}
