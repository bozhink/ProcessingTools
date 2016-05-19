namespace ProcessingTools.Web.Api.Controllers
{
    using System.Web.Http.Cors;
    using Bio.Taxonomy.Services.Data.Contracts;
    using Factories;

    [EnableCors("*", "*", "*")]
    public class GbifController : TaxaClassificationDataServiceControllerFactory
    {
        public GbifController(IGbifTaxaClassificationDataService service)
        {
            this.Service = service;
        }
    }
}