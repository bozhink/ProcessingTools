namespace ProcessingTools.MainProgram.Controllers
{
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Parse higher taxa using Aphia.")]
    public class ParseHigherTaxaWithAphiaController : ParseHigherTaxaWithDataServiceGenericController<IAphiaTaxaClassificationDataService>, IParseHigherTaxaWithAphiaController
    {
        public ParseHigherTaxaWithAphiaController(IAphiaTaxaClassificationDataService service)
            : base(service)
        {
        }
    }
}
