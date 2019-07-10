namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Web.Core.Api.Abstractions;

    [Route("api/[controller]")]
    [ApiController]
    public class CoLController : AbstractTaxonClassificationResolverController
    {
        protected CoLController(ICatalogueOfLifeTaxonClassificationResolver resolver, ILogger<CoLController> logger)
            : base(resolver, logger)
        {
        }
    }
}
