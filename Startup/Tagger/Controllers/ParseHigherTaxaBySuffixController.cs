namespace ProcessingTools.MainProgram.Controllers
{
    using Contracts;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Parse higher taxa by suffix.")]
    public class ParseHigherTaxaBySuffixController : ParseHigherTaxaWithDataServiceGenericController<ISuffixHigherTaxaRankDataService>, IParseHigherTaxaBySuffixController
    {
        public ParseHigherTaxaBySuffixController(ISuffixHigherTaxaRankDataService service)
            : base(service)
        {
        }
    }
}
