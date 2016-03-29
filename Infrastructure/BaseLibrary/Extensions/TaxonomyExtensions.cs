namespace ProcessingTools.BaseLibrary
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Bio.Taxonomy.Types;
    using DocumentProvider;
    using ProcessingTools.Configurator;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Extensions;

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
                XmlNodeList nodeList = xml.SelectNodes(xpath, TaxPubDocument.NamespceManager());
                if (stripTags)
                {
                    result = nodeList.Cast<XmlNode>().Select(c => c.TaxonNameXmlNodeToString()).Distinct().ToList();
                }
                else
                {
                    result = nodeList.GetStringListOfUniqueXmlNodes().ToList();
                }

                result.Sort();
            }

            return new HashSet<string>(result);
        }

        public static IEnumerable<string> ExtractUniqueHigherTaxa(this XmlDocument xmlDocument)
        {
            XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[@type='higher'][not(tn-part)]");
            return new HashSet<string>(nodeList.Cast<XmlNode>().Select(c => c.InnerText));
        }

        public static string GenerateTagTemplate(this string xml, Config config)
        {
            return xml.ApplyXslTransform(config.FloraGenerateTemplatesXslPath);
        }

        public static IEnumerable<string> GetListOfNonShortenedTaxa(this XmlNode xml)
        {
            ////string xpath = "//tp:taxon-name[count(tp:taxon-name-part[normalize-space(@full-name)=''])=0][tp:taxon-name-part[@taxon-name-part-type='genus']]";
            ////string xpath = "//tp:taxon-name[@type='lower'][not(tp:taxon-name-part[@full-name=''])][tp:taxon-name-part[@taxon-name-part-type='genus']]";
            string xpath = "//tn[@type='lower'][not(tn-part[@full-name=''])][tn-part[@type='genus']]";
            XmlDocument xd = new XmlDocument();
            XmlNamespaceManager nm = TaxPubDocument.NamespceManager();
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
            return new HashSet<string>(xml.GetStringListOfUniqueXmlNodes(xpath, TaxPubDocument.NamespceManager()));
        }

        public static IEnumerable<string> GetNonTaggedTaxa(this XmlDocument xml, Regex matchTaxa)
        {
            ////XmlNode clonedXml = xml.CloneNode(true);
            ////clonedXml.SelectNodes("//tn|//tn-part|//tp:taxon-name|//tp:taxon-name-part", Config.TaxPubNamespceManager())
            ////    .Cast<XmlNode>()
            ////    .Select(node => node.ParentNode.RemoveChild(node));

            IEnumerable<string> taxaMatchesInText = xml.InnerText.GetMatches(matchTaxa);
            IEnumerable<string> result = from item in taxaMatchesInText
                                         where xml.SelectNodes("//tn[contains(string(.),'" + item + "')]").Count == 0
                                         select item;
            return new HashSet<string>(result);

            ////IEnumerable<string> result = clonedXml.GetMatchesInXmlText(matchTaxa, true);

            ////return new HashSet<string>(result);
        }

        public static Task PrintNonParsedTaxa(this XmlDocument xmlDocument, ILogger logger)
        {
            return Task.Run(() =>
            {
                List<string> uniqueHigherTaxaList = xmlDocument.ExtractUniqueHigherTaxa().ToList();
                uniqueHigherTaxaList.Distinct();
                uniqueHigherTaxaList.TrimExcess();

                if (uniqueHigherTaxaList.Count() > 0)
                {
                    uniqueHigherTaxaList.Sort();

                    logger.Log("\nNon-parsed taxa:");
                    uniqueHigherTaxaList
                        .ForEach(taxon => logger?.Log("\t{0}", taxon));
                    logger.Log();
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
            foreach (XmlNode taxonName in xmlDocument.SelectNodes("//tn[name(..)!='tp:nomenclature']|//tp:taxon-name[name(..)!='tp:nomenclature']", TaxPubDocument.NamespceManager()))
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
                .RemoveXmlNodes("//object-id")
                .ReplaceXmlNodeInnerTextByItsFullNameAttribute()
                .InnerXml
                .RegexReplace(@"</[^>]*>(?=[^\s\)\]])(?!\Z)", " ")
                .RegexReplace(@"<[^>]+>", string.Empty)
                .RegexReplace(@"\s+", " ")
                .Trim();

            return innerXml;
        }
    }
}