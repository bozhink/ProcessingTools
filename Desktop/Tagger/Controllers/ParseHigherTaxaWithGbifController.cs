namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse higher taxa using GBIF.")]
    public class ParseHigherTaxaWithGbifController : ParseHigherTaxaWithDataServiceGenericController<IGbifTaxaRankResolverDataService>, IParseHigherTaxaWithGbifController
    {
        public ParseHigherTaxaWithGbifController(
            IDocumentFactory documentFactory,
            IHigherTaxaParserWithDataService<IGbifTaxaRankResolverDataService, ITaxonRank> parser)
            : base(documentFactory, parser)
        {
        }
    }
}
