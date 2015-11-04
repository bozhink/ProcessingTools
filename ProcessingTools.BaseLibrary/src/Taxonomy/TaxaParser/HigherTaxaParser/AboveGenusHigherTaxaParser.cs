namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using Configurator;
    using Contracts;

    public class AboveGenusHigherTaxaParser : HigherTaxaParser
    {
        private ILogger logger;
        private ITaxaRankResolver taxaRankResolver;

        public AboveGenusHigherTaxaParser(string xml, ITaxaRankResolver taxaRankResolver, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
            this.taxaRankResolver = taxaRankResolver;
        }

        public AboveGenusHigherTaxaParser(Config config, string xml, ITaxaRankResolver taxaRankResolver, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
            this.taxaRankResolver = taxaRankResolver;
        }

        public AboveGenusHigherTaxaParser(IBase baseObject, ITaxaRankResolver taxaRankResolver, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
            this.taxaRankResolver = taxaRankResolver;
        }

        /// <summary>
        /// This method parses all non-parsed higher taxa by making then of type 'above-genus'
        /// </summary>
        public override void Parse()
        {
            var uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();
            var resolvedTaxa = this.taxaRankResolver.Resolve(uniqueHigherTaxaList);
            foreach (var taxon in resolvedTaxa)
            {
                this.logger?.Log($"\n{taxon.ScientificName} --> {taxon.Rank}");

                string scientificNameReplacement = taxon.Rank.GetRemplacementStringForTaxonNamePartRank();
                this.ReplaceTaxonNameByItsParsedContent(taxon.ScientificName, scientificNameReplacement);
            }
        }
    }
}
