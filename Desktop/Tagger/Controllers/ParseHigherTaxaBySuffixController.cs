namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse higher taxa by suffix.")]
    public class ParseHigherTaxaBySuffixController : GenericParseHigherTaxaController<ISuffixHigherTaxaRankResolverDataService>, IParseHigherTaxaBySuffixController
    {
        public ParseHigherTaxaBySuffixController(
            IHigherTaxaParserWithDataService<ISuffixHigherTaxaRankResolverDataService, ITaxonRank> parser,
            ILogger logger)
            : base(parser, logger)
        {
        }
    }
}
