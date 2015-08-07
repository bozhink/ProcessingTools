using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace ProcessingTools.Base.Taxonomy
{
    public class TaxaParser : TaggerBase
    {
        private static string[] higherTaxaRanks = 
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

        private static string[] higherTaxaSuffixes = 
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

        public TaxaParser(string xml)
            : base(xml)
        {
        }

        public TaxaParser(Config config, string xml)
            : base(config, xml)
        {
        }

        public TaxaParser(TaggerBase baseObject)
            : base(baseObject)
        {
        }

        public static XmlDocument ParseHigherTaxaBySuffix(XmlDocument xmlDocument, bool nlmStyle)
        {
            XmlDocument xmlResult = xmlDocument;
            TaxonNamePart tnpart = new TaxonNamePart();
            string patternPrefix = "^([A-Z][a-z]*", patternSuffix = ")$";
            List<string> stringList = ExtractUniqueHigherTaxa(xmlDocument);
            foreach (string text in stringList)
            {
                Alert.Log(text);
                string replace = text;
                for (int i = 0; i < higherTaxaRanks.Length; i++)
                {
                    replace = Regex.Replace(
                        replace,
                        patternPrefix + higherTaxaSuffixes[i] + patternSuffix,
                        tnpart.TaxonNamePartReplace(higherTaxaRanks[i]));
                }

                xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(text) + ")(?=</tn>)", replace);
            }

            return xmlResult;
        }

        public static XmlDocument ParseHigherTaxaWithLocalDatabase(string databaseXmlFileName, XmlDocument xmlDocument, bool nlmStyle)
        {
            XmlDocument xmlResult = xmlDocument;
            TaxonNamePart tnpart = new TaxonNamePart();
            XElement rankList = XElement.Load(databaseXmlFileName);

            List<string> taxaList = ExtractUniqueHigherTaxa(xmlDocument);
            foreach (string taxon in taxaList)
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
            List<string> stringList = ExtractUniqueHigherTaxa(xmlDocument);
            bool delay = false;
            foreach (string text in stringList)
            {
                if (delay)
                {
                    System.Threading.Thread.Sleep(15000);
                }
                else
                {
                    delay = true;
                }

                Json.Gbif.GbifResult obj = Net.SearchGbif(text);
                if (obj != null)
                {
                    Console.WriteLine("\n{0} .... {1} .... {2}", text, obj.scientificName, obj.canonicalName);

                    if (obj.canonicalName != null || obj.scientificName != null)
                    {
                        if (!obj.canonicalName.Equals(text) && !obj.scientificName.Contains(text))
                        {
                            Alert.Log("No match.");
                        }
                        else
                        {
                            if (obj.rank != null)
                            {
                                Alert.Log("--> " + obj.rank.ToLower());
                                string replace = tnpart.TaxonNamePartReplace(obj.rank.ToLower());
                                xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(text) + ")(?=</tn>)", replace);
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
            List<string> stringList = ExtractUniqueHigherTaxa(xmlDocument);
            bool delay = false;
            foreach (string scientificName in stringList)
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
            List<string> stringList = ExtractUniqueHigherTaxa(xmlDocument);
            bool delay = false;
            foreach (string scientificName in stringList)
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
            List<string> stringList = ExtractUniqueHigherTaxa(xmlDocument);
            foreach (string text in stringList)
            {
                Alert.Log("\n" + text + " --> above-genus");
                string replace = tnpart.TaxonNamePartReplace("above-genus");
                xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(text) + ")(?=</tn>)", replace);
            }

            return xmlResult;
        }

        public static List<string> ExtractUniqueHigherTaxa(XmlDocument xmlDocument)
        {
            XmlNamespaceManager xmlNamespaceManager = ProcessingTools.Config.TaxPubNamespceManager(xmlDocument);
            XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[@type='higher'][not(tn-part)]", xmlNamespaceManager);
            return nodeList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();
        }

        public static string UnSplitTaxa(string content)
        {
            string result = Regex.Replace(content, @"(?<=full-name=""([^<>""]+)""[^>]*>)[^<>]*(?=</)", "$1");
            return Regex.Replace(result, "</?tn-part[^>]*>|</?tp:taxon-name-part[^>]*>", string.Empty);
        }

        public static string ParseRank(string infraSpecificRank)
        {
            string rank = string.Empty;
            switch (infraSpecificRank)
            {
                case "subgen":
                case "Subgen":
                case "subgenus":
                case "Subgenus":
                case "subg":
                case "Subg":
                    rank = "subgenus";
                    break;
                case "sect":
                case "Sect":
                case "section":
                case "Section":
                    rank = "section";
                    break;
                case "Ser":
                case "ser":
                    rank = "series";
                    break;
                case "subsect":
                case "Subsect":
                case "subsection":
                case "Subsection":
                    rank = "subsection";
                    break;
                case "subvar":
                case "Subvar":
                    rank = "subvariety";
                    break;
                case "var":
                case "Var":
                    rank = "variety";
                    break;
                case "subsp":
                case "Subsp":
                case "ssp":
                case "Ssp":
                case "subspec":
                case "Subspec":
                case "Subspecies":
                case "subspecies":
                    rank = "subspecies";
                    break;
                case "st":
                case "St":
                    rank = "stage";
                    break;
                case "r":
                    rank = "rank";
                    break;
                case "f":
                case "fo":
                case "form":
                case "Form":
                case "forma":
                case "Forma":
                    rank = "form";
                    break;
                case "sf":
                case "Sf":
                    rank = "subform";
                    break;
                case "aff":
                case "Aff":
                case "prope":
                case "Prope":
                case "cf":
                case "Cf":
                case "sp":
                case "Sp":
                case "nr":
                case "Nr":
                case "Near":
                case "near":
                case "sp. near":
                case "×":
                case "?":
                    rank = "species";
                    break;
                case "a":
                case "A":
                case "ab":
                case "Ab":
                    rank = "aberration";
                    break;
                default:
                    rank = "species";
                    break;
            }

            return rank;
        }

        public static string ParseLower(string str)
        {
            string replace = str;

            // Parse different parts of the taxonomic name
            replace = Regex.Replace(
                replace,
                @"\b([Ss]ubvar)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subvariety\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Vv]ar|v)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"variety\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Ff]orma?|f)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"form\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b(sf|[Ss]ubforma?)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subform\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Aa]b)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"aberration\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Ss]ubsp|[Ss]sp)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subspecies\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Aa]ff|[Cc]f|[Nn]r|[Ss]p|[Ss]p\.?\s*ne?a?r|[Ss]p\s+ne?a?r)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"uncertainty-rank\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Ss]ubgen|[Ss]ubgenus|[Ss]g)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subgenus\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Ss]ect|[Ss]ection)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"section\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Ss]ubsect|[Ss]ubsection)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subsection\">$3</tn-part>");

            // Parse taxa from beginnig

            // Genus species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>$4<tn-part type=\"subspecies\">$5</tn-part>");

            // Genus species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>");
            replace = Regex.Replace(
                replace,
                @"\A‘([A-Z][a-z\.]+)’([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "‘<tn-part type=\"genus\">$1</tn-part>’$2<tn-part type=\"species\">$3</tn-part>");

            // Genus (Subgenus) species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]+)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>$7<tn-part type=\"subspecies\">$8</tn-part>");

            // Genus (superspecies) species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]+)\(\s*([a-z][a-zçäöüëïâěôûêî\.-]+)([\s\?×]*?)\s*\)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"superspecies\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>$7<tn-part type=\"subspecies\">$8</tn-part>");

            // Genus (Subgenus) species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]+)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>");

            // Genus (superspecies) species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]+)\(\s*([a-z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"superspecies\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>");

            // Genus (Subgenus)
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]+)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)");

            // Genus
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)",
                "<tn-part type=\"genus\">$1</tn-part>");

            // species
            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)",
                @"<tn-part type=""species"">$1</tn-part>");

            // Try to parse whole string
            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ss]sp|[Ss]ubsp)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"subspecies\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Vv]ar|[Vv])(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"variety\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Aa]b)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"aberration\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ff]|[Ff]orma?)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"form\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ss]f|[Ss]ubforma?)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"subform\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([a-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"subspecies\">$2</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([A-Z\.-]+)\Z",
                "<tn-part type=\"genus\">$1</tn-part>");

            // Parse question mark
            replace = Regex.Replace(replace, @"(?<=</tn-part>)\s*\?", "<tn-part type=\"uncertainty-rank\">?</tn-part>");

            // Parse hybrid sign
            replace = Regex.Replace(replace, @"(?<=</tn-part>\s*)×(?=\s*<tn-part)", "<tn-part type=\"hybrid-sign\">×</tn-part>");

            // Here we must return the dot after tn-part[@type='infraspecific-rank'] back into the tag
            replace = Regex.Replace(replace, @"</tn-part>\.", ".</tn-part>");

            // Clear multiple white spaces between ‘tn-part’-s
            replace = Regex.Replace(replace, @"(?<=</tn-part>)\s{2,}(?=<tn-part)", " ");

            return replace;
        }

        /*
         * ================================================================================================
         * 
         * Lower taxa related methods
         * 
         * ================================================================================================
         */

        public void ParseLowerTaxa()
        {
            this.ParseXmlStringToXmlDocument();

            try
            {
                foreach (XmlNode node in this.XmlDocument.SelectNodes("//tn[@type='lower'][not(*)]", this.NamespaceManager))
                {
                    node.InnerXml = ParseLower(node.InnerXml);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Parse lower taxa without basionym.");
            }

            try
            {
                foreach (XmlNode node in this.XmlDocument.SelectNodes("//tn[@type='lower'][count(*) != count(tn-part)]", this.NamespaceManager))
                {
                    string replace = Regex.Replace(node.InnerXml, "</?i>", string.Empty);
                    string parseBasionym = Regex.Replace(replace, "^.*?<basionym>(.*?)</basionym>.*$", "$1");
                    parseBasionym = ParseLower(parseBasionym);
                    string parseSpecific = Regex.Replace(replace, "^.*?<specific>(.*?)</specific>.*$", "$1");
                    parseSpecific = ParseLower(parseSpecific);

                    replace = Regex.Replace(replace, "<genus>(.+?)</genus>", "<tn-part type=\"genus\">$1</tn-part>");
                    replace = Regex.Replace(replace, "<genus-authority>(.*?)</genus-authority>", "<tn-part type=\"authority\">$1</tn-part>");
                    replace = Regex.Replace(replace, "<basionym>(.*?)</basionym>", parseBasionym);
                    replace = Regex.Replace(replace, "<specific>(.*?)</specific>", parseSpecific);
                    replace = Regex.Replace(replace, @"<basionym-authority>(\s*)(\(.*?\))(\s*)(.*?)</basionym-authority>", "$1<tn-part type=\"basionym-authority\">$2</tn-part>$3<tn-part type=\"authority\">$4</tn-part>");
                    replace = Regex.Replace(replace, "<basionym-authority>(.*?)</basionym-authority>", "<tn-part type=\"authority\">$1</tn-part>");

                    for (Match m = Regex.Match(replace, @"<infraspecific-rank>[^<>]*</infraspecific-rank>\s*<infraspecific>[^<>]*</infraspecific>\s*\)?\s*<species>[^<>]*</species>(\s*<authority>[^<>]*</authority>)?"); m.Success; m = m.NextMatch())
                    {
                        string replace1 = m.Value;
                        string infraSpecificRank = Regex.Replace(Regex.Replace(replace, "^.*?<infraspecific-rank>([^<>]*)</infraspecific-rank>.*$", "$1"), "\\.", string.Empty);
                        string rank = ParseRank(infraSpecificRank);
                        replace1 = Regex.Replace(replace1, "<infraspecific-rank>([^<>]*)</infraspecific-rank>", "<tn-part type=\"infraspecific-rank\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, "<infraspecific>([^<>]*)</infraspecific>", "<tn-part type=\"" + rank + "\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, @"<species>([a-zçäöüëïâěôûêîæœ\.-]+)</species>", "<tn-part type=\"species\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, @"<species>([a-zçäöüëïâěôûêîæœ\.-]+)\s+([a-z\s\.-]+)</species>", "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"subspecies\">$2</tn-part>");
                        rank = replace1.Contains("type=\"subspecies\"") ? "subspecies" : "species";
                        replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", "<tn-part type=\"authority\">$1</tn-part>");
                        replace = Regex.Replace(replace, Regex.Escape(m.Value), replace1);
                    }

                    for (Match m = Regex.Match(replace, @"<infraspecific-rank>[^<>]*</infraspecific-rank>\s*<infraspecific>[^<>]*</infraspecific>(\s*<authority>[^<>]*</authority>)?"); m.Success; m = m.NextMatch())
                    {
                        string replace1 = m.Value;
                        string infraSpecificRank = Regex.Replace(Regex.Replace(replace, "^.*?<infraspecific-rank>([^<>]*)</infraspecific-rank>.*$", "$1"), "\\.", string.Empty);
                        string rank = ParseRank(infraSpecificRank);
                        replace1 = Regex.Replace(replace1, "<infraspecific-rank>([^<>]*)</infraspecific-rank>", "<tn-part type=\"infraspecific-rank\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, "<infraspecific>([^<>]*)</infraspecific>", "<tn-part type=\"" + rank + "\">$1</tn-part>");
                        ////replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", "<tn-part type=\"" + rank + "-authority\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", "<tn-part type=\"authority\">$1</tn-part>");
                        replace = Regex.Replace(replace, Regex.Escape(m.Value), replace1);
                    }

                    replace = Regex.Replace(replace, @"<sensu>(.*?)</sensu>", @"<tn-part type=""sensu"">$1</tn-part>");
                    replace = Regex.Replace(replace, @"<tn-part type=""infraspecific-rank"">×</tn-part>", @"<tn-part type=""hybrid-sign"">×</tn-part>");
                    replace = Regex.Replace(replace, @"<tn-part type=""infraspecific-rank"">\?</tn-part>", @"<tn-part type=""uncertainty-rank"">?</tn-part>");
                    replace = Regex.Replace(replace, @"<tn-part type=""infraspecific-rank"">((?i)(aff|prope|cf|nr|near|sp\. near)\.?)</tn-part>", @"<tn-part type=""uncertainty-rank"">$1</tn-part>");

                    node.InnerXml = replace;
                }

                // Add @full-name
                foreach (XmlNode node in this.XmlDocument.SelectNodes("//tn[@type='lower']/tn-part[not(@full-name)][@type!='sensu' and @type!='hybrid-sign' and @type!='uncertainty-rank' and @type!='infraspecific-rank' and @type!='authority' and @type!='basionym-authority'][contains(string(.), '.')]", this.NamespaceManager))
                {
                    XmlAttribute fullName = this.XmlDocument.CreateAttribute("full-name");
                    node.Attributes.Append(fullName);
                }

                // Add missing tags in lower-taxa
                foreach (XmlNode node in this.XmlDocument.SelectNodes("//tn[@type='lower'][not(count(tn-part)=1 and tn-part/@type='subgenus')][count(tn-part[@type='genus'])=0 or (count(tn-part[@type='species'])=0 and count(tn-part[@type!='genus'][@type!='subgenus'][@type!='section'][@type!='subsection'])!=0)]", this.NamespaceManager))
                {
                    XmlNode genus = node.SelectSingleNode(".//tn-part[@type='genus']", this.NamespaceManager);
                    if (genus == null)
                    {
                        XmlNode species = node.SelectSingleNode(".//tn-part[@type='species']", this.NamespaceManager);
                        if (species == null)
                        {
                            XmlElement speciesElement = this.XmlDocument.CreateElement("tn-part");

                            XmlAttribute elementType = this.XmlDocument.CreateAttribute("type");
                            elementType.InnerText = "species";
                            speciesElement.Attributes.Append(elementType);

                            XmlAttribute fullName = this.XmlDocument.CreateAttribute("full-name");
                            speciesElement.Attributes.Append(fullName);

                            node.PrependChild(speciesElement);
                        }

                        // Add genus tag
                        {
                            XmlElement genusElement = this.XmlDocument.CreateElement("tn-part");

                            XmlAttribute elementType = this.XmlDocument.CreateAttribute("type");
                            elementType.InnerText = "genus";
                            genusElement.Attributes.Append(elementType);

                            XmlAttribute fullName = this.XmlDocument.CreateAttribute("full-name");
                            genusElement.Attributes.Append(fullName);

                            node.PrependChild(genusElement);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Parse lower taxa with basionym.");
            }

            // Remove wrapping i around tn[tn-part[@type='subgenus']]
            this.XmlDocument.InnerXml = Regex.Replace(
                this.XmlDocument.InnerXml,
                @"<i>(<tn(\s*>|\s[^<>]*>)<tn-part type=""genus""[^<>]*>[^<>]*</tn-part>\s*\(<tn-part type=""(subgenus|superspecies)""[^<>]*>.*?</tn>)</i>",
                "$1");

            this.ParseXmlDocumentToXmlString();
        }

        /*
         * ================================================================================================
         * 
         * Higher taxa related methods
         * 
         * ================================================================================================
         */

        /// <summary>
        /// This is the main method to parse higher taxa. It applies parse-using-local-database-file by default.
        /// </summary>
        public void ParseHigherTaxa(
            bool parseWithDatabaseXmlFile = true,
            bool parseWithAphiaApi = false,
            bool parseWithCoLApi = false,
            bool parseWithGbifApi = false,
            bool parseBySuffix = false,
            bool parseAboveGenus = false)
        {
            this.ParseXmlStringToXmlDocument();

            if (parseWithDatabaseXmlFile)
            {
                this.XmlDocument = TaxaParser.ParseHigherTaxaWithLocalDatabase(this.Config.rankListXmlFilePath, this.XmlDocument, this.Config.NlmStyle);
            }

            if (parseWithAphiaApi)
            {
                this.XmlDocument = TaxaParser.ParseHigherTaxaWithAphiaApi(this.XmlDocument);
            }

            if (parseWithCoLApi)
            {
                this.XmlDocument = TaxaParser.ParseHigherTaxaWithCoLApi(this.XmlDocument);
            }

            if (parseWithGbifApi)
            {
                this.XmlDocument = TaxaParser.ParseHigherTaxaWithGbifApi(this.XmlDocument);
            }

            if (parseBySuffix)
            {
                this.XmlDocument = TaxaParser.ParseHigherTaxaBySuffix(this.XmlDocument, this.Config.NlmStyle);
            }

            if (parseAboveGenus)
            {
                this.XmlDocument = TaxaParser.ParseHigherTaxaWithAboveGenus(this.XmlDocument);
            }

            // Some debug information
            {
                List<string> stringList = ExtractUniqueHigherTaxa(this.XmlDocument);
                if (stringList.Count > 0)
                {
                    Alert.Log("\nNon-parsed taxa:");
                    foreach (string s in stringList)
                    {
                        Alert.Log("\t" + s);
                    }

                    Alert.Log();
                }
            }

            this.ParseXmlDocumentToXmlString();
        }

        public void UnSplitAllTaxa()
        {
            this.ParseXmlStringToXmlDocument();
            foreach (XmlNode node in this.XmlDocument.SelectNodes("//tn[name(..)!='tp:nomenclature']|//tp:taxon-name[name(..)!='tp:nomenclature']", this.NamespaceManager))
            {
                node.InnerXml = UnSplitTaxa(node.InnerXml);
            }

            this.ParseXmlDocumentToXmlString();
        }

        public class TaxonNamePart
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
