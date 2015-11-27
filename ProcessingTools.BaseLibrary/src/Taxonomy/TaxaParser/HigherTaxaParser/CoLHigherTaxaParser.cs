namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using Configurator;
    using Contracts.Log;
    using ServiceClient.Bio.CatalogueOfLife;

    public class CoLHigherTaxaParser : HigherTaxaParser
    {
        private ILogger logger;

        public CoLHigherTaxaParser(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public CoLHigherTaxaParser(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public CoLHigherTaxaParser(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public override void Parse()
        {
            try
            {
                IEnumerable<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();

                foreach (string scientificName in uniqueHigherTaxaList)
                {
                    this.Delay();

                    XmlDocument colResponse = CatalogueOfLifeDataRequester.SearchCatalogueOfLife(scientificName).Result;

                    this.logger?.Log($"\n{colResponse.OuterXml}\n");

                    XmlNodeList responseItems = colResponse.SelectNodes("/results/result[normalize-space(translate(name,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))='" + scientificName.ToLower() + "']");
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
                                this.logger?.Log($"{item["name"].InnerText} --> {item["rank"].InnerText}");
                            }
                        }
                        else
                        {
                            string rank = ranks[0];
                            this.logger?.Log($"{scientificName} = {responseItems[0]["name"].InnerText} --> {rank}");

                            string scientificNameReplacement = rank.GetRemplacementStringForTaxonNamePartRank();

                            this.ReplaceTaxonNameByItsParsedContent(scientificName, scientificNameReplacement);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
