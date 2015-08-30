namespace ProcessingTools.Base.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    public class HigherTaxaParserWithLocalDataBase : HigherTaxaParser
    {
        public HigherTaxaParserWithLocalDataBase(string xml)
            : base(xml)
        {
        }

        public HigherTaxaParserWithLocalDataBase(Config config, string xml)
            : base(config, xml)
        {
        }

        public HigherTaxaParserWithLocalDataBase(IBase baseObject)
            : base(baseObject)
        {
        }

        public override void Parse()
        {
            XElement rankList = XElement.Load(this.Config.rankListXmlFilePath);

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

                    string scientificNameReplacement = rank.GetRemplacementStringForTaxonNamePartRank();

                    this.ReplaceTaxonNameByItsParsedContent(scientificName, scientificNameReplacement);
                }
            }
        }
    }
}
