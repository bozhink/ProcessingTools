namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Api.Abstractions;

    public class AphiaController : AbstractTaxonClassificationResolverController
    {
        protected AphiaController(IAphiaTaxonClassificationResolver resolver, ILogger<AphiaController> logger)
            : base(resolver, logger)
        {
        }
    }
}
