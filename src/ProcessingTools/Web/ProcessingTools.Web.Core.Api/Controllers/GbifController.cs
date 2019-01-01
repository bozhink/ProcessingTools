namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Core.Api.Abstractions;

    [Route("api/[controller]")]
    [ApiController]
    public class GbifController : AbstractTaxonClassificationResolverController
    {
        protected GbifController(IGbifTaxonClassificationResolver resolver, ILogger<GbifController> logger)
            : base(resolver, logger)
        {
        }
    }
}
