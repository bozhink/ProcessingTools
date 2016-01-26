namespace ProcessingTools.MainProgram.Controllers
{
    using Contracts;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Parse higher taxa using GBIF.")]
    public class ParseHigherTaxaWithGbifController : ParseHigherTaxaWithDataServiceGenericController<IGbifTaxaClassificationDataService>, IParseHigherTaxaWithGbifController
    {
        public ParseHigherTaxaWithGbifController(IGbifTaxaClassificationDataService service)
            : base(service)
        {
        }
    }
}
