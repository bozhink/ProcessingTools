namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Make higher taxa of type 'above-genus'.")]
    public class ParseHigherTaxaAboveGenusController : ParseHigherTaxaWithDataServiceGenericController<IAboveGenusTaxaRankDataService>, IParseHigherTaxaAboveGenusController
    {
        public ParseHigherTaxaAboveGenusController(IAboveGenusTaxaRankDataService service)
            : base(service)
        {
        }
    }
}
