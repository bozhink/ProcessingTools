namespace ProcessingTools.Web.Api.Controllers
{
    using System.Web.Http.Cors;
    using Abstractions;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    [EnableCors("*", "*", "*")]
    public class GbifController : AbstractTaxaClassificationResolverController
    {
        public GbifController(IGbifTaxaClassificationResolver resolver)
            : base(resolver)
        {
        }
    }
}
