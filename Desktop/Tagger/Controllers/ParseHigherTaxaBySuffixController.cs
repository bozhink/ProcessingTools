namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    [Description("Parse higher taxa by suffix.")]
    public class ParseHigherTaxaBySuffixController : GenericParseHigherTaxaController<ISuffixHigherTaxaRankResolver>, IParseHigherTaxaBySuffixController
    {
        public ParseHigherTaxaBySuffixController(
            IHigherTaxaParserWithDataService<ISuffixHigherTaxaRankResolver, ITaxonRank> parser,
            ILogger logger)
            : base(parser, logger)
        {
        }
    }
}
