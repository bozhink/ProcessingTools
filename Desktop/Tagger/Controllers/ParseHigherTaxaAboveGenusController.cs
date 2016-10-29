namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Make higher taxa of type 'above-genus'.")]
    public class ParseHigherTaxaAboveGenusController : GenericParseHigherTaxaController<IAboveGenusTaxaRankResolverDataService>, IParseHigherTaxaAboveGenusController
    {
        public ParseHigherTaxaAboveGenusController(
            IHigherTaxaParserWithDataService<IAboveGenusTaxaRankResolverDataService, ITaxonRank> parser,
            ILogger logger)
            : base(parser, logger)
        {
        }
    }
}
