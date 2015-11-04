namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Configurator;
    using Contracts;

    public class LocalDataBaseHigherTaxaParser : HigherTaxaParser
    {
        private ILogger logger;

        public LocalDataBaseHigherTaxaParser(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public LocalDataBaseHigherTaxaParser(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public LocalDataBaseHigherTaxaParser(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public override void Parse()
        {
            XElement rankList = XElement.Load(this.Config.RankListXmlFilePath);

            IEnumerable<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                Regex searchTaxaName = new Regex("(?i)\\b" + scientificName + "\\b");
                IEnumerable<string> ranks = from item in rankList.Elements()
                                            where searchTaxaName.Match(item.Element("part").Element("value").Value).Success
                                            select item.Element("part").Element("rank").Element("value").Value;

                int ranksCount = (ranks == null) ? 0 : ranks.Count();
                if (ranksCount == 0)
                {
                    this.logger?.Log($"\n{scientificName} --> No match.");
                }
                else if (ranksCount > 1)
                {
                    this.logger?.Log(scientificName +
                        "\nWARNING: More than one records in local database." +
                        "\n         Substitution will not be performed.");

                    foreach (string rank in ranks)
                    {
                        this.logger?.Log($"\n\t{rank}");
                    }
                }
                else
                {
                    string rank = ranks.ElementAt(0);
                    this.logger?.Log($"\n{scientificName} --> {rank}");

                    string scientificNameReplacement = rank.GetRemplacementStringForTaxonNamePartRank();

                    this.ReplaceTaxonNameByItsParsedContent(scientificName, scientificNameReplacement);
                }
            }
        }
    }
}
