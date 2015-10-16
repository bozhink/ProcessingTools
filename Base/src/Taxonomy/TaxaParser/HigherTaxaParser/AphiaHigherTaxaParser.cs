namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using Configurator;
    using Globals;

    public class AphiaHigherTaxaParser : HigherTaxaParser
    {
        private ILogger logger;

        public AphiaHigherTaxaParser(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public AphiaHigherTaxaParser(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public AphiaHigherTaxaParser(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public override void Parse()
        {
            IEnumerable<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                this.Delay();

                XmlDocument aphiaResponse = Net.SearchAphia(scientificName);
                XmlNodeList responseItems = aphiaResponse.SelectNodes("//return/item[normalize-space(translate(scientificname,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))='" + scientificName.ToLower() + "']");
                if (responseItems.Count < 1)
                {
                    this.logger?.Log($"{scientificName} --> No match or error.");
                }
                else
                {
                    List<string> ranks = responseItems.Cast<XmlNode>().Select(c => c["rank"].InnerText.ToLower()).Distinct().ToList();
                    if (ranks.Count > 1)
                    {
                        this.logger?.Log($"WARNING:\n{scientificName} --> Multiple matches:");
                        foreach (XmlNode item in responseItems)
                        {
                            this.logger?.Log($"{item["scientificname"].InnerText} --> {item["rank"].InnerText}, {item["authority"].InnerText}");
                        }
                    }
                    else
                    {
                        string rank = ranks[0];
                        this.logger?.Log($"{scientificName} = {responseItems[0]["scientificname"].InnerText} --> {rank}");

                        string scientificNameReplacement = rank.GetRemplacementStringForTaxonNamePartRank();

                        this.ReplaceTaxonNameByItsParsedContent(scientificName, scientificNameReplacement);
                    }
                }
            }
        }
    }
}
