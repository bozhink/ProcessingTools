namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Parse higher taxa using Aphia.")]
    public class ParseHigherTaxaWithAphiaController : ParseHigherTaxaWithDataServiceGenericController<IAphiaTaxaRankResolverDataService>, IParseHigherTaxaWithAphiaController
    {
        public ParseHigherTaxaWithAphiaController(IAphiaTaxaRankResolverDataService service)
            : base(service)
        {
        }
    }
}
