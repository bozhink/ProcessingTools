namespace ProcessingTools.Web.Api.Controllers
{
    using Factories;
    using Geo.Services.Data.Contracts;
    using Geo.Services.Data.Models;
    using Models.GeoEpithets;

    public class GeoEpithetController : GenericDataServiceControllerFactory<GeoEpithetServiceModel, GeoEpithetRequestModel, GeoEpithetResponseModel>
    {
        public GeoEpithetController(IGeoEpithetsDataService service)
            : base(service)
        {
        }
    }
}