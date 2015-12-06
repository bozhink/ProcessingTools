namespace ProcessingTools.Web.Api.Controllers
{
    using System.Web.Http.Cors;
    using Bio.Taxonomy.Services.Data.Contracts;
    using Factories;

    [EnableCors("*", "*", "*")]
    public class CoLController : TaxaClassificationDataServiceControllerFactory
    {
        public CoLController(ICatalogueOfLifeTaxaClassificationDataService service)
        {
            this.Service = service;
        }
    }
}