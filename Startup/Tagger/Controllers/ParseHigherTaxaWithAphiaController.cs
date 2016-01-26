namespace ProcessingTools.MainProgram.Controllers
{
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public class ParseHigherTaxaWithAphiaController : ParseHigherTaxaWithDataServiceGenericController<IAphiaTaxaClassificationDataService>, IParseHigherTaxaWithAphiaController
    {
        public ParseHigherTaxaWithAphiaController(IAphiaTaxaClassificationDataService service)
            : base(service)
        {
        }
    }
}
