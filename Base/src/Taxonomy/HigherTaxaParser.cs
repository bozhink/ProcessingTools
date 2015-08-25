namespace ProcessingTools.Base.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Linq;

    public class HigherTaxaParser : TaggerBase, IParser
    {
        private static readonly string[] higherTaxaRanks =
            {
                "phylum",
                "subphylum",
                "class",
                "subclass",
                "superorder",
                "order",
                "suborder",
                "infraorder",
                "superfamily",
                "epifamily",
                "family",
                "subfamily",
                "tribe",
                "subtribe"
            };

        private static readonly string[] higherTaxaSuffixes =
            {
                "phyta|mycota",
                "phytina|mycotina",
                "ia|opsida|phyceae|mycetes",
                "idae|phycidae|mycetidae",
                "anae",
                "ales",
                "ineae",
                "aria",
                "acea|oidea",
                "oidae",
                "aceae|idae",
                "oideae|inae",
                "eae|ini",
                "inae|ina"
            };

        public HigherTaxaParser(string xml)
            : base(xml)
        {
        }

        public HigherTaxaParser(Config config, string xml)
            : base(config, xml)
        {
        }

        public HigherTaxaParser(IBase baseObject)
            : base(baseObject)
        {
        }

        public static XmlDocument ParseHigherTaxaBySuffix(XmlDocument xmlDocument, bool nlmStyle)
        {
            XmlDocument xmlResult = xmlDocument;
            TaxonNamePart tnpart = new TaxonNamePart();
            string patternPrefix = "^([A-Z][a-z]*", patternSuffix = ")$";
            List<string> uniqueHigherTaxaList = xmlDocument.ExtractUniqueHigherTaxa();
            foreach (string taxon in uniqueHigherTaxaList)
            {
                Alert.Log(taxon);
                string replace = taxon;
                for (int i = 0; i < higherTaxaRanks.Length; i++)
                {
                    replace = Regex.Replace(
                        replace,
                        patternPrefix + higherTaxaSuffixes[i] + patternSuffix,
                        tnpart.TaxonNamePartReplace(higherTaxaRanks[i]));
                }

                xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(taxon) + ")(?=</tn>)", replace);
            }

            return xmlResult;
        }

        public static XmlDocument ParseHigherTaxaWithLocalDatabase(string databaseXmlFileName, XmlDocument xmlDocument, bool nlmStyle)
        {
            XmlDocument xmlResult = xmlDocument;
            TaxonNamePart tnpart = new TaxonNamePart();
            XElement rankList = XElement.Load(databaseXmlFileName);

            List<string> uniqueHigherTaxaList = xmlDocument.ExtractUniqueHigherTaxa();
            foreach (string taxon in uniqueHigherTaxaList)
            {
                Regex searchTaxaName = new Regex("(?i)\\b" + taxon + "\\b");
                IEnumerable<string> ranks = from item in rankList.Elements()
                                            where searchTaxaName.Match(item.Element("part").Element("value").Value).Success
                                            select item.Element("part").Element("rank").Element("value").Value;
                int ranksCount = (ranks == null) ? 0 : ranks.Count();
                if (ranksCount == 0)
                {
                    Alert.Log("\n" + taxon + " --> No match.");
                }
                else if (ranksCount > 1)
                {
                    Alert.Log(taxon +
                        "\nWARNING: More than one records in local database." +
                        "\n         Substitution will not be performed.");
                    foreach (string rank in ranks)
                    {
                        Alert.Log("\n\t" + rank);
                    }
                }
                else
                {
                    string rank = ranks.ElementAt(0);
                    Alert.Log("\n" + taxon + " --> " + rank);
                    string replace = tnpart.TaxonNamePartReplace(rank);
                    xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(taxon) + ")(?=</tn>)", replace);
                }
            }

            return xmlResult;
        }

        public static XmlDocument ParseHigherTaxaWithGbifApi(XmlDocument xmlDocument)
        {
            XmlDocument xmlResult = xmlDocument;
            TaxonNamePart tnpart = new TaxonNamePart();
            List<string> uniqueHigherTaxaList = xmlDocument.ExtractUniqueHigherTaxa();
            bool delay = false;
            foreach (string taxon in uniqueHigherTaxaList)
            {
                if (delay)
                {
                    System.Threading.Thread.Sleep(15000);
                }
                else
                {
                    delay = true;
                }

                Json.Gbif.GbifResult obj = Net.SearchGbif(taxon);
                if (obj != null)
                {
                    Console.WriteLine("\n{0} .... {1} .... {2}", taxon, obj.scientificName, obj.canonicalName);

                    if (obj.canonicalName != null || obj.scientificName != null)
                    {
                        if (!obj.canonicalName.Equals(taxon) && !obj.scientificName.Contains(taxon))
                        {
                            Alert.Log("No match.");
                        }
                        else
                        {
                            if (obj.rank != null)
                            {
                                Alert.Log("--> " + obj.rank.ToLower());
                                string replace = tnpart.TaxonNamePartReplace(obj.rank.ToLower());
                                xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(taxon) + ")(?=</tn>)", replace);
                            }
                        }
                    }
                }
            }

            return xmlResult;
        }

        public static XmlDocument ParseHigherTaxaWithAphiaApi(XmlDocument xmlDocument)
        {
            XmlDocument xmlResult = xmlDocument;
            TaxonNamePart tnpart = new TaxonNamePart();
            List<string> uniqueHigherTaxaList = xmlDocument.ExtractUniqueHigherTaxa();
            bool delay = false;
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                if (delay)
                {
                    System.Threading.Thread.Sleep(15000);
                }
                else
                {
                    delay = true;
                }

                XmlDocument response = Net.SearchAphia(scientificName);
                XmlNodeList responseItems = response.SelectNodes("//return/item[normalize-space(translate(scientificname,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))='" + scientificName.ToLower() + "']");
                if (responseItems.Count < 1)
                {
                    Alert.Log(scientificName + " --> No match or error.");
                }
                else
                {
                    List<string> ranks = responseItems.Cast<XmlNode>().Select(c => c["rank"].InnerText.ToLower()).Distinct().ToList();
                    if (ranks.Count > 1)
                    {
                        Alert.Log("WARNING:\n" + scientificName + " --> Multiple matches:");
                        foreach (XmlNode item in responseItems)
                        {
                            Alert.Log(item["scientificname"].InnerText + " --> " + item["rank"].InnerText + ", " + item["authority"].InnerText);
                        }
                    }
                    else
                    {
                        string rank = ranks[0];
                        Alert.Log(scientificName + " = " + responseItems[0]["scientificname"].InnerText + " --> " + rank);
                        string replace = tnpart.TaxonNamePartReplace(rank);
                        xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(scientificName) + ")(?=</tn>)", replace);
                    }
                }
            }

            return xmlResult;
        }

        public static XmlDocument ParseHigherTaxaWithCoLApi(XmlDocument xmlDocument)
        {
            XmlDocument xmlResult = xmlDocument;
            TaxonNamePart tnpart = new TaxonNamePart();
            List<string> uniqueHigherTaxaList = xmlDocument.ExtractUniqueHigherTaxa();
            bool delay = false;
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                if (delay)
                {
                    System.Threading.Thread.Sleep(15000);
                }
                else
                {
                    delay = true;
                }

                XmlDocument response = Net.SearchCatalogueOfLife(scientificName);

                Alert.Log("\n" + response.OuterXml + "\n");

                XmlNodeList responseItems = response.SelectNodes("/results/result[normalize-space(translate(name,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))='" + scientificName.ToLower() + "']");
                if (responseItems.Count < 1)
                {
                    Alert.Log(scientificName + " --> No match or error.");
                }
                else
                {
                    List<string> ranks = responseItems.Cast<XmlNode>().Select(c => c["rank"].InnerText.ToLower()).Distinct().ToList();
                    if (ranks.Count > 1)
                    {
                        Alert.Log("WARNING:\n" + scientificName + " --> Multiple matches:");
                        foreach (XmlNode item in responseItems)
                        {
                            Alert.Log(item["name"].InnerText + " --> " + item["rank"].InnerText);
                        }
                    }
                    else
                    {
                        string rank = ranks[0].ToLower();
                        Alert.Log(scientificName + " = " + responseItems[0]["name"].InnerText + " --> " + rank);
                        string replace = tnpart.TaxonNamePartReplace(rank);
                        xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(scientificName) + ")(?=</tn>)", replace);
                    }
                }
            }

            return xmlResult;
        }

        /// <summary>
        /// This method parses all non-parsed higher taxa by making then of type 'above-genus'
        /// </summary>
        /// <param name="xmlDocument">Xml document to be processed</param>
        /// <returns>Processed xml document</returns>
        public static XmlDocument ParseHigherTaxaWithAboveGenus(XmlDocument xmlDocument)
        {
            XmlDocument xmlResult = xmlDocument;
            TaxonNamePart tnpart = new TaxonNamePart();
            List<string> uniqueHigherTaxaList = xmlDocument.ExtractUniqueHigherTaxa();
            foreach (string text in uniqueHigherTaxaList)
            {
                Alert.Log("\n" + text + " --> above-genus");
                string replace = tnpart.TaxonNamePartReplace("above-genus");
                xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(text) + ")(?=</tn>)", replace);
            }

            return xmlResult;
        }

        public void Parse()
        {
            this.Parse(true);
        }

        /// <summary>
        /// This is the main method to parse higher taxa. It applies parse-using-local-database-file by default.
        /// </summary>
        public void Parse(
            bool parseWithDatabaseXmlFile = true,
            bool parseWithAphiaApi = false,
            bool parseWithCoLApi = false,
            bool parseWithGbifApi = false,
            bool parseBySuffix = false,
            bool parseAboveGenus = false)
        {
            if (parseWithDatabaseXmlFile)
            {
                this.XmlDocument = ParseHigherTaxaWithLocalDatabase(this.Config.rankListXmlFilePath, this.XmlDocument, this.Config.NlmStyle);
            }

            if (parseWithAphiaApi)
            {
                this.XmlDocument = ParseHigherTaxaWithAphiaApi(this.XmlDocument);
            }

            if (parseWithCoLApi)
            {
                this.XmlDocument = ParseHigherTaxaWithCoLApi(this.XmlDocument);
            }

            if (parseWithGbifApi)
            {
                this.XmlDocument = ParseHigherTaxaWithGbifApi(this.XmlDocument);
            }

            if (parseBySuffix)
            {
                this.XmlDocument = ParseHigherTaxaBySuffix(this.XmlDocument, this.Config.NlmStyle);
            }

            if (parseAboveGenus)
            {
                this.XmlDocument = ParseHigherTaxaWithAboveGenus(this.XmlDocument);
            }

            // Some debug information
            {
                List<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();
                if (uniqueHigherTaxaList.Count > 0)
                {
                    Alert.Log("\nNon-parsed taxa:");
                    foreach (string s in uniqueHigherTaxaList)
                    {
                        Alert.Log("\t" + s);
                    }

                    Alert.Log();
                }
            }
        }

        // TODO: remove this class
        private class TaxonNamePart
        {
            private string prefix, suffix;
            private bool taxPub;

            public TaxonNamePart(bool taxPub = false)
            {
                this.taxPub = taxPub;
                if (taxPub)
                {
                    this.prefix = "<tp:taxon-name-part taxon-name-part-type=\"";
                    this.suffix = "\">$1</tp:taxon-name-part>";
                }
                else
                {
                    this.prefix = "<tn-part type=\"";
                    this.suffix = "\">$1</tn-part>";
                }
            }

            public string Prefix
            {
                get
                {
                    return this.prefix;
                }
            }

            public string Suffix
            {
                get
                {
                    return this.suffix;
                }
            }

            public string TaxonNamePartReplace(string rank)
            {
                return this.prefix + rank + this.suffix;
            }
        }
    }
}
