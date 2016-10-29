namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse higher taxa using Aphia.")]
    public class ParseHigherTaxaWithAphiaController : GenericParseHigherTaxaController<IAphiaTaxaRankResolverDataService>, IParseHigherTaxaWithAphiaController
    {
        public ParseHigherTaxaWithAphiaController(
            IHigherTaxaParserWithDataService<IAphiaTaxaRankResolverDataService, ITaxonRank> parser,
            ILogger logger)
            : base(parser, logger)
        {
        }
    }
}
