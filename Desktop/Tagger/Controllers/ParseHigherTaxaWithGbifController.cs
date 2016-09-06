namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Parse higher taxa using GBIF.")]
    public class ParseHigherTaxaWithGbifController : ParseHigherTaxaWithDataServiceGenericController<IGbifTaxaRankResolverDataService>, IParseHigherTaxaWithGbifController
    {
        public ParseHigherTaxaWithGbifController(IGbifTaxaRankResolverDataService service)
            : base(service)
        {
        }
    }
}
