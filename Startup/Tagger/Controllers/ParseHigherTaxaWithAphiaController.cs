namespace ProcessingTools.MainProgram.Controllers
{
    using Contracts;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Parse higher taxa using Aphia.")]
    public class ParseHigherTaxaWithAphiaController : ParseHigherTaxaWithDataServiceGenericController<IAphiaTaxaClassificationDataService>, IParseHigherTaxaWithAphiaController
    {
        public ParseHigherTaxaWithAphiaController(IAphiaTaxaClassificationDataService service)
            : base(service)
        {
        }
    }
}
