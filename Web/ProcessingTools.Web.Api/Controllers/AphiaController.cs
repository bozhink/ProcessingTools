namespace ProcessingTools.Web.Api.Controllers
{
    using Bio.Taxonomy.Services.Data.Contracts;
    using Factories;

    public class AphiaController : TaxaClassificationDataServiceControllerFactory
    {
        public AphiaController(IAphiaTaxaClassificationResolverDataService service)
        {
            this.Service = service;
        }
    }
}