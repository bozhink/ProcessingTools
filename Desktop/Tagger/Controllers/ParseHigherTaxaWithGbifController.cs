namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse higher taxa using GBIF.")]
    public class ParseHigherTaxaWithGbifController : GenericParseHigherTaxaController<IGbifTaxaRankResolverDataService>, IParseHigherTaxaWithGbifController
    {
        public ParseHigherTaxaWithGbifController(
            IHigherTaxaParserWithDataService<IGbifTaxaRankResolverDataService, ITaxonRank> parser,
            ILogger logger)
            : base(parser, logger)
        {
        }
    }
}
