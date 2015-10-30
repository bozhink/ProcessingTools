namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using Configurator;
    using Contracts;
    using ServiceClient.Aphia;

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
            try
            {
                IEnumerable<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();
                foreach (string scientificName in uniqueHigherTaxaList)
                {
                    this.Delay();

                    var aphiaService = new AphiaNameService();

                    var aphiaRecords = aphiaService.getAphiaRecords(scientificName, false, true, false, 0);

                    if (aphiaRecords == null || aphiaRecords.Length < 1)
                    {
                        this.logger?.Log($"{scientificName} --> No match or error.");
                    }
                    else
                    {
                        var ranks = new HashSet<string>(aphiaRecords.Where(x => string.Compare(x.scientificname, scientificName, true) == 0).Select(x => x.rank.ToLower()));
                        if (ranks.Count > 1)
                        {
                            this.logger?.Log($"WARNING:\n{scientificName} --> Multiple matches:");
                            foreach (var record in aphiaRecords)
                            {
                                this.logger?.Log($"{record.scientificname} --> {record.rank}, {record.authority}");
                            }
                        }
                        else
                        {
                            string rank = ranks.ElementAt(0);

                            this.logger?.Log($"{scientificName} = {aphiaRecords[0].scientificname} --> {rank}");

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
