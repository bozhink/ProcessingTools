namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse higher taxa with local database.")]
    public class ParseHigherTaxaWithLocalDbController : GenericParseHigherTaxaController<ILocalDbTaxaRankResolverDataService>, IParseHigherTaxaWithLocalDbController
    {
        public ParseHigherTaxaWithLocalDbController(
            IHigherTaxaParserWithDataService<ILocalDbTaxaRankResolverDataService, ITaxonRank> parser,
            ILogger logger)
            : base(parser, logger)
        {
        }
    }
}
