namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Api.Abstractions;

    public class GbifController : AbstractTaxonClassificationResolverController
    {
        protected GbifController(IGbifTaxonClassificationResolver resolver, ILogger<GbifController> logger)
            : base(resolver, logger)
        {
        }
    }
}
