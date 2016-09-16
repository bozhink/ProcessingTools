namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Make higher taxa of type 'above-genus'.")]
    public class ParseHigherTaxaAboveGenusController : ParseHigherTaxaWithDataServiceGenericController<IAboveGenusTaxaRankResolverDataService>, IParseHigherTaxaAboveGenusController
    {
        public ParseHigherTaxaAboveGenusController(IHigherTaxaParserWithDataService<IAboveGenusTaxaRankResolverDataService, ITaxonRank> parser)
            : base(parser)
        {
        }
    }
}
