namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Api.Abstractions;

    public class CoLController : AbstractTaxonClassificationResolverController
    {
        protected CoLController(ICatalogueOfLifeTaxonClassificationResolver resolver, ILogger<CoLController> logger)
            : base(resolver, logger)
        {
        }
    }
}
