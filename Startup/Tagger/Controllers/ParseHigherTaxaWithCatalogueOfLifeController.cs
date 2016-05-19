namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Parse higher taxa using CoL.")]
    public class ParseHigherTaxaWithCatalogueOfLifeController : ParseHigherTaxaWithDataServiceGenericController<ICatalogueOfLifeTaxaClassificationDataService>, IParseHigherTaxaWithCatalogueOfLifeController
    {
        public ParseHigherTaxaWithCatalogueOfLifeController(ICatalogueOfLifeTaxaClassificationDataService service)
            : base(service)
        {
        }
    }
}
