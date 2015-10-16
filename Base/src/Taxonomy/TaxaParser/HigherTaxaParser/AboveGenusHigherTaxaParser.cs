namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using Configurator;
    using Globals;

    public class AboveGenusHigherTaxaParser : HigherTaxaParser
    {
        private const string Rank = "above-genus";

        private ILogger logger;

        public AboveGenusHigherTaxaParser(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public AboveGenusHigherTaxaParser(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public AboveGenusHigherTaxaParser(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        /// <summary>
        /// This method parses all non-parsed higher taxa by making then of type 'above-genus'
        /// </summary>
        public override void Parse()
        {
            string scientificNameReplacement = Rank.GetRemplacementStringForTaxonNamePartRank();

            IEnumerable<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                this.logger?.Log($"\n{scientificName} --> {Rank}");

                this.ReplaceTaxonNameByItsParsedContent(scientificName, scientificNameReplacement);
            }
        }
    }
}
