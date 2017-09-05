namespace ProcessingTools.Web.Api.Controllers
{
    using Abstractions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    public class AphiaController : AbstractTaxaClassificationResolverController<IAphiaTaxaClassificationResolver>
    {
        protected AphiaController(ITaxaClassificationResolver resolver, ILogger logger)
            : base(resolver, logger)
        {
        }
    }
}
