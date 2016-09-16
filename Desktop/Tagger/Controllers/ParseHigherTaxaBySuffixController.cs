namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Parse higher taxa by suffix.")]
    public class ParseHigherTaxaBySuffixController : ParseHigherTaxaWithDataServiceGenericController<ISuffixHigherTaxaRankResolverDataService>, IParseHigherTaxaBySuffixController
    {
        public ParseHigherTaxaBySuffixController(IHigherTaxaParserWithDataService<ISuffixHigherTaxaRankResolverDataService, ITaxonRank> parser)
            : base(parser)
        {
        }
    }
}
