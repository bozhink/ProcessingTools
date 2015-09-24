namespace ProcessingTools.BaseLibrary
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
            if (xpath != string.Empty)
            {
                XmlNodeList nodeList = xml.SelectNodes(xpath, Config.TaxPubNamespceManager());
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

        private static string TaxonNameXmlNodeToString(this XmlNode taxonNameNode)
        {
            XmlNode result = taxonNameNode.CloneNode(true);
            result.Attributes.RemoveAll();

            string innerXml = result
                .RemoveXmlNodes("//object-id")
                .ReplaceXmlNodeInnerTextByItsFullNameAttribute()
                .InnerXml;

            innerXml = Regex.Replace(innerXml, @"</[^>]*>(?=[^\s\)\]])(?!\Z)", " ");
            innerXml = Regex.Replace(innerXml, @"<[^>]+>|[\(\)\[\]]", string.Empty);

            return innerXml;
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

        public static string ExtractTaxa(this string xml, Config config)
        {
            return xml.ApplyXslTransform(config.floraExtractTaxaXslPath);
        }

        public static IEnumerable<string> ExtractUniqueHigherTaxa(this XmlDocument xmlDocument)
        {
            XmlNamespaceManager xmlNamespaceManager = Config.TaxPubNamespceManager(xmlDocument);
            XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[@type='higher'][not(tn-part)]", xmlNamespaceManager);
            return new HashSet<string>(nodeList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct());
        }

        public static string GenerateTagTemplate(this string xml, Config config)
        {
            return xml.ApplyXslTransform(config.floraGenerateTemplatesXslPath);
        }

        public static IEnumerable<string> GetListOfNonShortenedTaxa(this XmlNode xml)
        {
            ////string xpath = "//tp:taxon-name[count(tp:taxon-name-part[normalize-space(@full-name)=''])=0][tp:taxon-name-part[@taxon-name-part-type='genus']]";
            ////string xpath = "//tp:taxon-name[@type='lower'][not(tp:taxon-name-part[@full-name=''])][tp:taxon-name-part[@taxon-name-part-type='genus']]";
            string xpath = "//tn[@type='lower'][not(tn-part[@full-name=''])][tn-part[@type='genus']]";
            XmlDocument xd = new XmlDocument();
            XmlNamespaceManager nm = Config.TaxPubNamespceManager();
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
            return new HashSet<string>(xml.GetStringListOfUniqueXmlNodes(xpath, Config.TaxPubNamespceManager()));
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

        public static void PrintNonParsedTaxa(this XmlDocument xmlDocument, ILogger logger)
        {
            IEnumerable<string> uniqueHigherTaxaList = xmlDocument.ExtractUniqueHigherTaxa();
            if (uniqueHigherTaxaList.Count() > 0)
            {
                logger.Log("\nNon-parsed taxa:");
                foreach (string taxon in uniqueHigherTaxaList)
                {
                    logger.Log("\t" + taxon);
                }

                logger.Log();
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

        public static void RemoveTaxaInWrongPlaces(this XmlDocument xml)
        {
            string xpath = "tn[.//tn] | a[.//tn] | ext-link[.//tn] | xref[.//tn] | article/front/notes/sec[.//tn] | tp:treatment-meta/kwd-group/kwd/named-content[.//tn] | *[@object_id='82'][.//tn] | *[@id='41'][.//tn] | *[@id='236' or @id='436' or @id='435' or @id='418' or @id='49' or @id='417' or @id='48' or @id='434' or @id='433' or @id='432' or @id='431' or @id='430' or @id='429' or @id='428' or @id='427' or @id='426' or @id='425' or @id='424' or @id='423' or @id='422' or @id='421' or @id='420' or @id='419' or @id='475' or @id='414']/value[.//tn]";

            Regex matchTaxonTag = new Regex(@"</?tn[^>]*>");

            foreach (XmlNode node in xml.SelectNodes(xpath, Config.TaxPubNamespceManager()))
            {
                node.InnerXml = matchTaxonTag.Replace(node.InnerXml, string.Empty);
            }
        }

        public static IEnumerable<string> GetNonTaggedTaxa(this XmlDocument xml, Regex matchTaxa)
        {
            ////XmlNode clonedXml = xml.CloneNode(true);
            ////clonedXml.SelectNodes("//tn|//tn-part|//tp:taxon-name|//tp:taxon-name-part", Config.TaxPubNamespceManager())
            ////    .Cast<XmlNode>()
            ////    .Select(node => node.ParentNode.RemoveChild(node));

            IEnumerable<string> taxaMatchesInText = xml.GetMatchesInXmlText(matchTaxa, true);
            IEnumerable<string> result = from item in taxaMatchesInText
                                         where xml.SelectNodes("//tn[contains(string(.),'" + item + "')]").Count == 0
                                         select item;
            return new HashSet<string>(result);

            ////IEnumerable<string> result = clonedXml.GetMatchesInXmlText(matchTaxa, true);

            ////return new HashSet<string>(result);
        }
    }
}