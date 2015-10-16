namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using Configurator;
    using Globals;
    using Json.Gbif;

    public class GbifHigherTaxaParser : HigherTaxaParser
    {
        private ILogger logger;

        public GbifHigherTaxaParser(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public GbifHigherTaxaParser(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public GbifHigherTaxaParser(IBase baseObject, ILogger logger)
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


                    GbifResult gbifResult = Net.SearchGbif(scientificName);
                    if (gbifResult != null)
                    {
                        this.logger?.Log("\n{0} .... {1} .... {2}", scientificName, gbifResult.scientificName, gbifResult.canonicalName);

                        if (gbifResult.canonicalName != null || gbifResult.scientificName != null)
                        {
                            if (!gbifResult.canonicalName.Equals(scientificName) && !gbifResult.scientificName.Contains(scientificName))
                            {
                                this.logger?.Log("No match.");
                            }
                            else
                            {
                                string rank = gbifResult.rank;
                                if (rank != null && rank != string.Empty)
                                {
                                    rank = rank.ToLower();
                                    this.logger?.Log($"{scientificName} --> {rank}");

                                    string scientificNameReplacement = rank.GetRemplacementStringForTaxonNamePartRank();

                                    this.ReplaceTaxonNameByItsParsedContent(scientificName, scientificNameReplacement);
                                }
                            }
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
