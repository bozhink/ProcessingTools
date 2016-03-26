namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Parse higher taxa using GBIF.")]
    public class ParseHigherTaxaWithGbifController : ParseHigherTaxaWithDataServiceGenericController<IGbifTaxaClassificationDataService>, IParseHigherTaxaWithGbifController
    {
        public ParseHigherTaxaWithGbifController(IGbifTaxaClassificationDataService service)
            : base(service)
        {
        }
    }
}
