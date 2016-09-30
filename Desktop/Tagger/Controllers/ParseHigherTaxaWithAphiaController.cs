namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse higher taxa using Aphia.")]
    public class ParseHigherTaxaWithAphiaController : ParseHigherTaxaWithDataServiceGenericController<IAphiaTaxaRankResolverDataService>, IParseHigherTaxaWithAphiaController
    {
        public ParseHigherTaxaWithAphiaController(
            IDocumentFactory documentFactory,
            IHigherTaxaParserWithDataService<IAphiaTaxaRankResolverDataService, ITaxonRank> parser)
            : base(documentFactory, parser)
        {
        }
    }
}
