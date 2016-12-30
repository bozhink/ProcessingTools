namespace ProcessingTools.Web.Api.Controllers
{
    using Abstractions;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    public class AphiaController : AbstractTaxaClassificationResolverController
    {
        public AphiaController(IAphiaTaxaClassificationResolver resolver)
            : base(resolver)
        {
        }
    }
}
