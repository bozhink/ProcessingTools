namespace ProcessingTools.Web.Api.Controllers
{
    using Bio.Taxonomy.Services.Data.Contracts;
    using Factories;

    public class AphiaController : TaxaClassificationDataServiceControllerFactory
    {
        public AphiaController(IAphiaTaxaClassificationDataService service)
        {
            this.Service = service;
        }
    }
}