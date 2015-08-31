namespace ProcessingTools.Base.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    public class AphiaHigherTaxaParser : HigherTaxaParser
    {
        public AphiaHigherTaxaParser(string xml)
            : base(xml)
        {
        }

        public AphiaHigherTaxaParser(Config config, string xml)
            : base(config, xml)
        {
        }

        public AphiaHigherTaxaParser(IBase baseObject)
            : base(baseObject)
        {
        }

        public override void Parse()
        {
            List<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();

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

                        string scientificNameReplacement = rank.GetRemplacementStringForTaxonNamePartRank();

                        this.ReplaceTaxonNameByItsParsedContent(scientificName, scientificNameReplacement);
                    }
                }
            }
        }
    }
}
