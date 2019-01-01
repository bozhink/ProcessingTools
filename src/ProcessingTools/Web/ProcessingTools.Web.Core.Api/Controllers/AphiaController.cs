namespace ProcessingTools.Web.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Core.Api.Abstractions;

    [Route("api/[controller]")]
    [ApiController]
    public class AphiaController : AbstractTaxonClassificationResolverController
    {
        protected AphiaController(IAphiaTaxonClassificationResolver resolver, ILogger<AphiaController> logger)
            : base(resolver, logger)
        {
        }
    }
}
