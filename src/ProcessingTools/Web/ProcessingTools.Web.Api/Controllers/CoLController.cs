namespace ProcessingTools.Web.Api.Controllers
{
    using System.Web.Http.Cors;
    using Abstractions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Data.Bio.Taxonomy;

    [EnableCors("*", "*", "*")]
    public class CoLController : AbstractTaxaClassificationResolverController<ICatalogueOfLifeTaxaClassificationResolver>
    {
        protected CoLController(ITaxaClassificationResolver resolver, ILogger logger)
            : base(resolver, logger)
        {
        }
    }
}
