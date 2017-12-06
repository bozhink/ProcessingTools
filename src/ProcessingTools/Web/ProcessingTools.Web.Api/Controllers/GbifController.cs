namespace ProcessingTools.Web.Api.Controllers
{
    using System.Web.Http.Cors;
    using Abstractions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;

    [EnableCors("*", "*", "*")]
    public class GbifController : AbstractTaxaClassificationResolverController<IGbifTaxaClassificationResolver>
    {
        protected GbifController(ITaxaClassificationResolver resolver, ILogger logger)
            : base(resolver, logger)
        {
        }
    }
}
