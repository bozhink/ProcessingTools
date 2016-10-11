namespace ProcessingTools.BaseLibrary
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;
    using ProcessingTools.Extensions;
    using ProcessingTools.Xml.Extensions;

    public static class TaxonomyExtensions
    {
        public static IEnumerable<string> ExtractTaxa(this XmlNode xml, bool stripTags = false, TaxaType type = TaxaType.Any)
        {
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

            List<string> result = new List<string>();
            if (!string.IsNullOrWhiteSpace(xpath))
            {
                XmlNodeList nodeList = xml.SelectNodes(xpath, TaxPubXmlNamespaceManagerProvider.GetStatic());
                if (stripTags)
                {
                    result = nodeList.Cast<XmlNode>().Select(c => c.TaxonNameXmlNodeToString()).Distinct().ToList();
                }
                else
                {
                    result = nodeList.GetStringListOfUniqueXmlNodes().ToList();
                }
            }

            return new HashSet<string>(result);
        }

        public static IEnumerable<string> ExtractUniqueNonParsedHigherTaxa(this XmlNode context)
        {
            var taxaNames = context.SelectNodes(".//tn[@type='higher'][not(tn-part)]")
                .Cast<XmlNode>()
                .Select(c => c.InnerText);

            var result = new HashSet<string>(taxaNames);

            return result;
        }

        public static IEnumerable<string> GetListOfNonShortenedTaxa(this XmlNode xml)
        {
            ////string xpath = "//tp:taxon-name[count(tp:taxon-name-part[normalize-space(@full-name)=''])=0][tp:taxon-name-part[@taxon-name-part-type='genus']]";
            ////string xpath = "//tp:taxon-name[@type='lower'][not(tp:taxon-name-part[@full-name=''])][tp:taxon-name-part[@taxon-name-part-type='genus']]";
            string xpath = "//tn[@type='lower'][not(tn-part[@full-name=''])][tn-part[@type='genus']]";
            XmlDocument xd = new XmlDocument();
            XmlNamespaceManager nm = TaxPubXmlNamespaceManagerProvider.GetStatic();
            XmlNodeList nodeList = xml.SelectNodes(xpath, nm);
            List<XmlNode> newList = new List<XmlNode>();
            foreach (XmlNode node in nodeList)
            {
                XmlNode taxonName = xd.CreateElement("tn");
                foreach (XmlNode innerNode in node.SelectNodes(".//*", nm))
                {
                    XmlNode newNode = xd.CreateElement("tn-part");
                    foreach (XmlAttribute attribute in innerNode.Attributes)
                    {
                        if (attribute.Name.Contains("type"))
                        {
                            XmlAttribute newAttribute = xd.CreateAttribute(attribute.Name);
                            newAttribute.InnerText = attribute.InnerText;
                            newNode.Attributes.Append(newAttribute);
                        }
                    }

                    if (innerNode.Attributes["full-name"] != null)
                    {
                        newNode.InnerText = innerNode.Attributes["full-name"].InnerText;
                    }
                    else
                    {
                        newNode.InnerText = innerNode.InnerText;
                    }

                    taxonName.AppendChild(newNode);
                }

                newList.Add(taxonName);
            }

            return new HashSet<string>(newList.GetStringListOfUniqueXmlNodes());
        }

        public static IEnumerable<string> GetListOfShortenedTaxa(this XmlNode xml)
        {
            ////string xpath = "//tp:taxon-name[@type='lower'][tp:taxon-name-part[@full-name[normalize-space(.)='']]][tp:taxon-name-part[@taxon-name-part-type='genus']][normalize-space(tp:taxon-name-part[@taxon-name-part-type='species'])!='']";
            string xpath = "//tn[@type='lower'][tn-part[@full-name[normalize-space(.)='']][normalize-space(.)!='']][tn-part[@type='genus']][normalize-space(tn-part[@type='species'])!='']";
            return new HashSet<string>(xml.GetStringListOfUniqueXmlNodes(xpath, TaxPubXmlNamespaceManagerProvider.GetStatic()));
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

        public static string RemoveTaxonNamePartTags(this string content)
        {
            string result = Regex.Replace(content, @"(?<=full-name=""([^<>""]+)""[^>]*>)[^<>]*(?=</)", "$1");
            return Regex.Replace(result, "</?tn-part[^>]*>|</?tp:taxon-name-part[^>]*>", string.Empty);
        }

        public static void RemoveTaxonNamePartTags(this XmlDocument xmlDocument)
        {
            foreach (XmlNode taxonName in xmlDocument.SelectNodes("//tn[name(..)!='tp:nomenclature']|//tp:taxon-name[name(..)!='tp:nomenclature']", TaxPubXmlNamespaceManagerProvider.GetStatic()))
            {
                taxonName.InnerXml = taxonName.InnerXml.RemoveTaxonNamePartTags();
            }
        }

        public static XmlNode ReplaceXmlNodeInnerTextByItsFullNameAttribute(this XmlNode node)
        {
            foreach (XmlNode fullNamedPart in node.SelectNodes(".//*[normalize-space(@full-name)!='']"))
            {
                fullNamedPart.InnerText = fullNamedPart.Attributes["full-name"].InnerText;
                fullNamedPart.Attributes.RemoveNamedItem("full-name");
            }

            return node;
        }

        private static string TaxonNameXmlNodeToString(this XmlNode taxonNameNode)
        {
            XmlNode result = taxonNameNode.CloneNode(true);
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