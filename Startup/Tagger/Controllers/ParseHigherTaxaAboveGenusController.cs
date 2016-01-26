namespace ProcessingTools.MainProgram.Controllers
{
    using Contracts;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Make higher taxa of type 'above-genus'.")]
    public class ParseHigherTaxaAboveGenusController : ParseHigherTaxaWithDataServiceGenericController<IAboveGenusTaxaRankDataService>, IParseHigherTaxaAboveGenusController
    {
        public ParseHigherTaxaAboveGenusController(IAboveGenusTaxaRankDataService service)
            : base(service)
        {
        }
    }
}
