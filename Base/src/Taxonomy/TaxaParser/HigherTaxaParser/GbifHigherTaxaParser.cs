namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using Configurator;
    using Contracts;
    using ServiceClient.Gbif;

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

                    var gbifResult = GbifDataRequester.SearchGbif(scientificName).Result;

                    if (gbifResult != null)
                    {
                        this.logger?.Log("\n{0} .... {1} .... {2}", scientificName, gbifResult.ScientificName, gbifResult.CanonicalName);

                        if (gbifResult.CanonicalName != null || gbifResult.ScientificName != null)
                        {
                            if (!gbifResult.CanonicalName.Equals(scientificName) && !gbifResult.ScientificName.Contains(scientificName))
                            {
                                this.logger?.Log("No match.");
                            }
                            else
                            {
                                string rank = gbifResult.Rank;
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
