namespace ProcessingTools.Web.Api.Controllers
{
    using System.Web.Http.Cors;
    using Abstractions;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    [EnableCors("*", "*", "*")]
    public class CoLController : AbstractTaxaClassificationResolverController
    {
        public CoLController(ICatalogueOfLifeTaxaClassificationResolver resolver)
            : base(resolver)
        {
        }
    }
}
