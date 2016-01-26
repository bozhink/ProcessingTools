namespace ProcessingTools.MainProgram.Controllers
{
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public class ParseHigherTaxaWithCatalogueOfLifeController : ParseHigherTaxaWithDataServiceGenericController<ICatalogueOfLifeTaxaClassificationDataService>, IParseHigherTaxaWithCatalogueOfLifeController
    {
        public ParseHigherTaxaWithCatalogueOfLifeController(ICatalogueOfLifeTaxaClassificationDataService service)
            : base(service)
        {
        }
    }
}
