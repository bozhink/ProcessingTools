namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Parse higher taxa using CoL.")]
    public class ParseHigherTaxaWithCatalogueOfLifeController : ParseHigherTaxaWithDataServiceGenericController<ICatalogueOfLifeTaxaClassificationDataService>, IParseHigherTaxaWithCatalogueOfLifeController
    {
        public ParseHigherTaxaWithCatalogueOfLifeController(ICatalogueOfLifeTaxaClassificationDataService service)
            : base(service)
        {
        }
    }
}
