namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider.Extensions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Strings.Extensions;
    using ProcessingTools.Xml.Extensions;

    public static class TaxonomyExtensions
    {
        public static IEnumerable<string> ExtractTaxa(this XmlNode node, bool stripTags = false, TaxaType type = TaxaType.Any)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            string typeString = type.ToString().ToLower();
            string xpath = string.Empty;
            switch (type)
            {
                case TaxaType.Lower:
                case TaxaType.Higher:
                    xpath = string.Format("//tn[@type='{0}']|//tp:taxon-name[@type='{0}']", typeString);
                    break;

                case TaxaType.Any:
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
                    result = nodeList.GetStringListOfUniqueXmlNodes().ToList();
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

        public static IEnumerable<string> GetListOfNonShortenedTaxa(this XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var document = (node is XmlDocument ? node : node.OwnerDocument) as XmlDocument;

            ////string xpath = "//tp:taxon-name[count(tp:taxon-name-part[normalize-space(@full-name)=''])=0][tp:taxon-name-part[@taxon-name-part-type='genus']]";
            ////string xpath = "//tp:taxon-name[@type='lower'][not(tp:taxon-name-part[@full-name=''])][tp:taxon-name-part[@taxon-name-part-type='genus']]";
            string xpath = "//tn[@type='lower'][not(tn-part[@full-name=''])][tn-part[@type='genus']]";
            var result = node.SelectNodesWithTaxPubXmlNamespaceManager(xpath)
                .Cast<XmlNode>()
                .Select(currentNode =>
                {
                    XmlElement taxonNameElement = document.CreateElement("tn");
                    foreach (XmlNode innerNode in currentNode.SelectNodes(".//*"))
                    {
                        XmlElement taxonNamePartElement = document.CreateElement("tn-part");

                        // Copy only *type* attributes
                        foreach (XmlAttribute attribute in innerNode.Attributes)
                        {
                            if (attribute.Name.Contains("type"))
                            {
                                XmlAttribute typeAttribute = document.CreateAttribute(attribute.Name);
                                typeAttribute.InnerText = attribute.InnerText;
                                taxonNamePartElement.Attributes.Append(typeAttribute);
                            }
                        }

                        // Gets the value of the @full-name attribute if present or the content of the node
                        var fullNameAttribute = innerNode.Attributes["full-name"];
                        if (fullNameAttribute != null && !string.IsNullOrWhiteSpace(fullNameAttribute.InnerText))
                        {
                            taxonNamePartElement.InnerText = fullNameAttribute.InnerText;
                        }
                        else
                        {
                            taxonNamePartElement.InnerText = innerNode.InnerText;
                        }

                        taxonNameElement.AppendChild(taxonNamePartElement);
                    }

                    return taxonNameElement;
                })
                .ToList<XmlNode>()
                .GetStringListOfUniqueXmlNodes();
            

            return new HashSet<string>(result);
        }

        public static IEnumerable<string> GetListOfShortenedTaxa(this XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            ////string xpath = "//tp:taxon-name[@type='lower'][tp:taxon-name-part[@full-name[normalize-space(.)='']]][tp:taxon-name-part[@taxon-name-part-type='genus']][normalize-space(tp:taxon-name-part[@taxon-name-part-type='species'])!='']";
            string xpath = "//tn[@type='lower'][tn-part[@full-name[normalize-space(.)='']][normalize-space(.)!='']][tn-part[@type='genus']][normalize-space(tn-part[@type='species'])!='']";
            var result = node.GetStringListOfUniqueXmlNodes(xpath, node.GetTaxPubXmlNamespaceManager());

            return new HashSet<string>(result);
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

        public static XmlNode ReplaceXmlNodeInnerTextByItsFullNameAttribute(this XmlNode node)
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