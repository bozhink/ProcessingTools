namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Api.Abstractions;

    public class GbifController : AbstractTaxaClassificationResolverController
    {
        protected GbifController(IGbifTaxaClassificationResolver resolver, ILogger<GbifController> logger)
            : base(resolver, logger)
        {
        }
    }
}
