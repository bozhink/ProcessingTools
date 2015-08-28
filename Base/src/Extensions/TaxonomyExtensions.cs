namespace ProcessingTools.Base
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Taxonomy;

    public static class TaxonomyExtensions
    {
        public static string DistinctTaxa(this string xml, Config config)
        {
            return xml.ApplyXslTransform(config.floraDistrinctTaxaXslPath);
        }

        public static List<string> ExtractTaxa(this XmlNode xml, bool stripTags = false, TaxaType type = TaxaType.Any)
        {
            List<string> result = new List<string>();
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

            if (xpath != string.Empty)
            {
                XmlNodeList nodeList = xml.SelectNodes(xpath, Config.TaxPubNamespceManager());
                if (stripTags)
                {
                    result = nodeList.Cast<XmlNode>().Select(c => c.TaxonNameXmlNodeToString()).Distinct().ToList();
                }
                else
                {
                    result = nodeList.GetStringListOfUniqueXmlNodes();
                }

                result.Sort();
            }

            return result;
        }

        private static string TaxonNameXmlNodeToString(this XmlNode taxonNameNode)
        {
            XmlNode result = taxonNameNode.CloneNode(true);
            result.Attributes.RemoveAll();

            foreach (XmlNode fullNamedPart in result.SelectNodes(".//*[normalize-space(@full-name)!='']"))
            {
                fullNamedPart.InnerText = fullNamedPart.Attributes["full-name"].InnerText;
                fullNamedPart.Attributes.RemoveNamedItem("full-name");
            }

            string innerXml = result.InnerXml;

            innerXml = Regex.Replace(innerXml, @"</[^>]*>(?=[^\s\)\]])(?!\Z)", " ");
            innerXml = Regex.Replace(innerXml, @"<[^>]+>|[\(\)\[\]]", string.Empty);

            return innerXml;
        }

        public static string ExtractTaxa(this string xml, Config config)
        {
            return xml.ApplyXslTransform(config.floraExtractTaxaXslPath);
        }

        public static List<string> ExtractUniqueHigherTaxa(this XmlDocument xmlDocument)
        {
            XmlNamespaceManager xmlNamespaceManager = Config.TaxPubNamespceManager(xmlDocument);
            XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[@type='higher'][not(tn-part)]", xmlNamespaceManager);
            return nodeList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();
        }

        public static string GenerateTagTemplate(this string xml, Config config)
        {
            return xml.ApplyXslTransform(config.floraGenerateTemplatesXslPath);
        }

        public static List<string> GetListOfNonShortenedTaxa(this XmlNode xml)
        {
            ////string xpath = "//tp:taxon-name[count(tp:taxon-name-part[normalize-space(@full-name)=''])=0][tp:taxon-name-part[@taxon-name-part-type='genus']]";
            ////string xpath = "//tp:taxon-name[@type='lower'][not(tp:taxon-name-part[@full-name=''])][tp:taxon-name-part[@taxon-name-part-type='genus']]";
            string xpath = "//tn[@type='lower'][not(tn-part[@full-name=''])][tn-part[@type='genus']]";
            XmlDocument xd = new XmlDocument();
            XmlNamespaceManager nm = ProcessingTools.Config.TaxPubNamespceManager();
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

            return newList.GetStringListOfUniqueXmlNodes();
        }

        public static List<string> GetListOfShortenedTaxa(this XmlNode xml)
        {
            ////string xpath = "//tp:taxon-name[@type='lower'][tp:taxon-name-part[@full-name[normalize-space(.)='']]][tp:taxon-name-part[@taxon-name-part-type='genus']][normalize-space(tp:taxon-name-part[@taxon-name-part-type='species'])!='']";
            string xpath = "//tn[@type='lower'][tn-part[@full-name[normalize-space(.)='']][normalize-space(.)!='']][tn-part[@type='genus']][normalize-space(tn-part[@type='species'])!='']";
            return xml.GetStringListOfUniqueXmlNodes(xpath, ProcessingTools.Config.TaxPubNamespceManager());
        }

        public static string GetRemplacementStringForTaxonNamePartRank(this string rank, bool taxPub = false)
        {
            string prefix, suffix;
            if (taxPub)
            {
                prefix = "<tp:taxon-name-part taxon-name-part-type=\"";
                suffix = "\">$1</tp:taxon-name-part>";
            }
            else
            {
                prefix = "<tn-part type=\"";
                suffix = "\">$1</tn-part>";
            }

            return prefix + rank + suffix;
        }

        public static void PrintNonParsedTaxa(this XmlDocument xmlDocument)
        {
            List<string> uniqueHigherTaxaList = xmlDocument.ExtractUniqueHigherTaxa();
            if (uniqueHigherTaxaList.Count > 0)
            {
                Alert.Log("\nNon-parsed taxa:");
                foreach (string taxon in uniqueHigherTaxaList)
                {
                    Alert.Log("\t" + taxon);
                }

                Alert.Log();
            }
        }

        public static string RemoveTaxonNamePartTags(this string content)
        {
            string result = Regex.Replace(content, @"(?<=full-name=""([^<>""]+)""[^>]*>)[^<>]*(?=</)", "$1");
            return Regex.Replace(result, "</?tn-part[^>]*>|</?tp:taxon-name-part[^>]*>", string.Empty);
        }

        public static void RemoveTaxonNamePartTags(this XmlDocument xmlDocument)
        {
            foreach (XmlNode taxonName in xmlDocument.SelectNodes("//tn[name(..)!='tp:nomenclature']|//tp:taxon-name[name(..)!='tp:nomenclature']", Config.TaxPubNamespceManager(xmlDocument)))
            {
                taxonName.InnerXml = taxonName.InnerXml.RemoveTaxonNamePartTags();
            }
        }
    }
}