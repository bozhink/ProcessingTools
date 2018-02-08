namespace ProcessingTools.Web.Api.Controllers
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Api.Abstractions;

    public class GbifController : AbstractTaxaClassificationResolverController
    {
        protected GbifController(IGbifTaxaClassificationResolver resolver, ILogger logger)
            : base(resolver, logger)
        {
        }
    }
}
