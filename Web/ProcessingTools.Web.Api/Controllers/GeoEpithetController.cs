namespace ProcessingTools.Web.Api.Controllers
{
    using Factories;
    using Geo.Services.Data.Contracts;
    using Geo.Services.Data.Models.Contracts;
    using Models.GeoEpithets;

    public class GeoEpithetController : GenericDataServiceControllerFactory<IGeoEpithetServiceModel, GeoEpithetRequestModel, GeoEpithetResponseModel>
    {
        public GeoEpithetController(IGeoEpithetsDataService service)
            : base(service)
        {
        }
    }
}