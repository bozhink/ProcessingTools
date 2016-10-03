namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse higher taxa using CoL.")]
    public class ParseHigherTaxaWithCatalogueOfLifeController : ParseHigherTaxaWithDataServiceGenericController<ICatalogueOfLifeTaxaRankResolverDataService>, IParseHigherTaxaWithCatalogueOfLifeController
    {
        public ParseHigherTaxaWithCatalogueOfLifeController(
            IDocumentFactory documentFactory,
            IHigherTaxaParserWithDataService<ICatalogueOfLifeTaxaRankResolverDataService, ITaxonRank> parser,
            ILogger logger)
            : base(documentFactory, parser, logger)
        {
        }
    }
}
