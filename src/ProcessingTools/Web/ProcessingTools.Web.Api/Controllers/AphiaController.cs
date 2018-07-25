namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Api.Abstractions;

    public class AphiaController : AbstractTaxaClassificationResolverController
    {
        protected AphiaController(IAphiaTaxaClassificationResolver resolver, ILogger<AphiaController> logger)
            : base(resolver, logger)
        {
        }
    }
}
