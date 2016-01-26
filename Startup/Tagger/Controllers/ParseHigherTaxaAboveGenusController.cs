namespace ProcessingTools.MainProgram.Controllers
{
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public class ParseHigherTaxaAboveGenusController : ParseHigherTaxaWithDataServiceGenericController<IAboveGenusTaxaRankDataService>, IParseHigherTaxaAboveGenusController
    {
        public ParseHigherTaxaAboveGenusController(IAboveGenusTaxaRankDataService service)
            : base(service)
        {
        }
    }
}
