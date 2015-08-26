namespace ProcessingTools.Base.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Linq;

    public class HigherTaxaParser : Base, IParser
    {
        private readonly string[] higherTaxaRanks =
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

        private readonly string[] higherTaxaSuffixes =
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

        private bool delay = false;

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
                this.ParseHigherTaxaWithLocalDatabase(this.Config.rankListXmlFilePath);
            }

            if (parseWithAphiaApi)
            {
                this.ParseHigherTaxaWithAphiaApi();
            }

            if (parseWithCoLApi)
            {
                this.ParseHigherTaxaWithCoLApi();
            }

            if (parseWithGbifApi)
            {
                this.ParseHigherTaxaWithGbifApi();
            }

            if (parseBySuffix)
            {
                this.ParseHigherTaxaBySuffix();
            }

            if (parseAboveGenus)
            {
                this.ParseHigherTaxaWithAboveGenus();
            }

            // Some debug information
            {
                List<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();
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
        }

        private void ParseHigherTaxaBySuffix()
        {
            string patternPrefix = "^([A-Z][a-z]*", patternSuffix = ")$";
            List<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                Alert.Log(scientificName);
                string replace = scientificName;
                for (int i = 0; i < this.higherTaxaRanks.Length; ++i)
                {
                    string pattern = patternPrefix + this.higherTaxaSuffixes[i] + patternSuffix;
                    string replacement = this.higherTaxaRanks[i].GetRemplacementStringForTaxonNamePartRank();

                    replace = Regex.Replace(replace, pattern, replacement);
                }

                this.ReplaceTaxonNameByItsParsedContent(scientificName, replace);
            }
        }

        private void ParseHigherTaxaWithLocalDatabase(string databaseXmlFileName)
        {
            XElement rankList = XElement.Load(databaseXmlFileName);

            List<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                Regex searchTaxaName = new Regex("(?i)\\b" + scientificName + "\\b");
                IEnumerable<string> ranks = from item in rankList.Elements()
                                            where searchTaxaName.Match(item.Element("part").Element("value").Value).Success
                                            select item.Element("part").Element("rank").Element("value").Value;

                int ranksCount = (ranks == null) ? 0 : ranks.Count();
                if (ranksCount == 0)
                {
                    Alert.Log("\n" + scientificName + " --> No match.");
                }
                else if (ranksCount > 1)
                {
                    Alert.Log(scientificName +
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
                    Alert.Log("\n" + scientificName + " --> " + rank);

                    string replacement = rank.GetRemplacementStringForTaxonNamePartRank();

                    this.ReplaceTaxonNameByItsParsedContent(scientificName, replacement);
                }
            }
        }

        private void ParseHigherTaxaWithGbifApi()
        {
            List<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();

            this.delay = false;
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                this.Delay();

                Json.Gbif.GbifResult gbifResult = Net.SearchGbif(scientificName);
                if (gbifResult != null)
                {
                    Console.WriteLine("\n{0} .... {1} .... {2}", scientificName, gbifResult.scientificName, gbifResult.canonicalName);

                    if (gbifResult.canonicalName != null || gbifResult.scientificName != null)
                    {
                        if (!gbifResult.canonicalName.Equals(scientificName) && !gbifResult.scientificName.Contains(scientificName))
                        {
                            Alert.Log("No match.");
                        }
                        else
                        {
                            string rank = gbifResult.rank;
                            if (rank != null && rank != string.Empty)
                            {
                                rank = rank.ToLower();
                                Alert.Log("--> " + rank);

                                string replacement = rank.GetRemplacementStringForTaxonNamePartRank();

                                this.ReplaceTaxonNameByItsParsedContent(scientificName, replacement);
                            }
                        }
                    }
                }
            }
        }

        private void ParseHigherTaxaWithAphiaApi()
        {
            List<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();

            this.delay = false;
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                this.Delay();

                XmlDocument aphiaResponse = Net.SearchAphia(scientificName);
                XmlNodeList responseItems = aphiaResponse.SelectNodes("//return/item[normalize-space(translate(scientificname,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))='" + scientificName.ToLower() + "']");
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

                        string replacement = rank.GetRemplacementStringForTaxonNamePartRank();

                        this.ReplaceTaxonNameByItsParsedContent(scientificName, replacement);
                    }
                }
            }
        }

        private void ParseHigherTaxaWithCoLApi()
        {
            List<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();

            this.delay = false;
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                this.Delay();

                XmlDocument colResponse = Net.SearchCatalogueOfLife(scientificName);

                Alert.Log("\n" + colResponse.OuterXml + "\n");

                XmlNodeList responseItems = colResponse.SelectNodes("/results/result[normalize-space(translate(name,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))='" + scientificName.ToLower() + "']");
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

                        string replacement = rank.GetRemplacementStringForTaxonNamePartRank();

                        this.ReplaceTaxonNameByItsParsedContent(scientificName, replacement);
                    }
                }
            }
        }

        /// <summary>
        /// This method parses all non-parsed higher taxa by making then of type 'above-genus'
        /// </summary>
        /// <param name="xmlDocument">Xml document to be processed</param>
        /// <returns>Processed xml document</returns>
        public void ParseHigherTaxaWithAboveGenus()
        {
            string rank = "above-genus";
            string replacement = rank.GetRemplacementStringForTaxonNamePartRank();

            List<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                Alert.Log("\n" + scientificName + " --> " + rank);

                this.ReplaceTaxonNameByItsParsedContent(scientificName, replacement);
            }
        }

        private void Delay()
        {
            if (this.delay)
            {
                System.Threading.Thread.Sleep(15000);
            }
            else
            {
                this.delay = true;
            }
        }

        private void ReplaceTaxonNameByItsParsedContent(string scientificName, string replacement)
        {
            this.XmlDocument.InnerXml = Regex.Replace(this.XmlDocument.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(scientificName) + ")(?=</tn>)", replacement);
        }
    }
}
