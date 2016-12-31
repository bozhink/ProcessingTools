namespace ProcessingTools.Bio.Taxonomy.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider.Extensions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Strings.Extensions;
    using ProcessingTools.Xml.Extensions;

    // TODO: Consolidate different extension namespace-s
    public static class TaxonomyExtensions
    {
        public static XmlElement CreateTaxonNameXmlElement(this IDocument document, TaxonType type)
        {
            XmlElement tn = document.XmlDocument.CreateElement(ElementNames.TaxonName);
            tn.SetAttribute(AttributeNames.Type, type.ToString().ToLower());
            return tn;
        }

        public static IEnumerable<string> ExtractTaxa(this XmlNode node, bool stripTags = false, TaxonType type = TaxonType.Any)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
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
                XmlNodeList nodeList = node.SelectNodesWithTaxPubXmlNamespaceManager(xpath);
                if (stripTags)
                {
                    result = nodeList.Cast<XmlNode>()
                        .Select(c => c.TaxonNameXmlNodeToString())
                        .Distinct()
                        .ToList();
                }
                else
                {
                    result = nodeList.Cast<XmlNode>()
                        .Select(c => c.InnerXml)
                        .Distinct()
                        .ToList();
                }
            }

            return new HashSet<string>(result);
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

        public static Task PrintNonParsedTaxa(this XmlDocument xmlDocument, ILogger logger)
        {
            return Task.Run(() =>
            {
                var uniqueHigherTaxaList = xmlDocument.ExtractUniqueNonParsedHigherTaxa()
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();

                if (uniqueHigherTaxaList.Count > 0)
                {
                    logger?.Log("\nNon-parsed taxa: {0}\n", string.Join("\n\t", uniqueHigherTaxaList));
                }
            });
        }

        private static XmlNode ReplaceXmlNodeInnerTextByItsFullNameAttribute(this XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            foreach (XmlNode fullNamedPart in node.SelectNodes(".//*[normalize-space(@full-name)!='']"))
            {
                fullNamedPart.InnerText = fullNamedPart.Attributes["full-name"].InnerText;
                fullNamedPart.Attributes.RemoveNamedItem("full-name");
            }

            return node;
        }

        private static string TaxonNameXmlNodeToString(this XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            XmlNode result = node.CloneNode(true);
            result.Attributes.RemoveAll();

            string innerXml = result
                .RemoveXmlNodes(".//object-id|.//tn-part[@type='uncertainty-rank']")
                .ReplaceXmlNodeInnerTextByItsFullNameAttribute()
                .InnerXml
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
    }
}
