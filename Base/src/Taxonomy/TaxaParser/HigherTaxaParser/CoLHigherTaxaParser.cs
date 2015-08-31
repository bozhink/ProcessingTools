namespace ProcessingTools.Base.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    public class CoLHigherTaxaParser : HigherTaxaParser
    {
        public CoLHigherTaxaParser(string xml)
            : base(xml)
        {
        }

        public CoLHigherTaxaParser(Config config, string xml)
            : base(config, xml)
        {
        }

        public CoLHigherTaxaParser(IBase baseObject)
            : base(baseObject)
        {
        }

        public override void Parse()
        {
            List<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();

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
                        string rank = ranks[0];
                        Alert.Log(scientificName + " = " + responseItems[0]["name"].InnerText + " --> " + rank);

                        string scientificNameReplacement = rank.GetRemplacementStringForTaxonNamePartRank();

                        this.ReplaceTaxonNameByItsParsedContent(scientificName, scientificNameReplacement);
                    }
                }
            }
        }
    }
}
