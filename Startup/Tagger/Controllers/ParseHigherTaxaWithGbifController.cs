namespace ProcessingTools.MainProgram.Controllers
{
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public class ParseHigherTaxaWithGbifController : ParseHigherTaxaWithDataServiceGenericController<IGbifTaxaClassificationDataService>, IParseHigherTaxaWithGbifController
    {
        public ParseHigherTaxaWithGbifController(IGbifTaxaClassificationDataService service)
            : base(service)
        {
        }
    }
}
