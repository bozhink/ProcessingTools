namespace ProcessingTools.MainProgram.Controllers
{
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public class ParseHigherTaxaBySuffixController : ParseHigherTaxaWithDataServiceGenericController<ISuffixHigherTaxaRankDataService>, IParseHigherTaxaBySuffixController
    {
        public ParseHigherTaxaBySuffixController(ISuffixHigherTaxaRankDataService service)
            : base(service)
        {
        }
    }
}
