namespace ProcessingTools.Web.Api.Controllers
{
    using Factories;
    using Geo.Services.Data.Contracts;
    using Geo.Services.Data.Models.Contracts;
    using Models.GeoEpithetModels;

    public class GeoEpithetController : GenericDataServiceControllerFactory<IGeoEpithet, GeoEpithetRequestModel, GeoEpithetResponseModel>
    {
        public GeoEpithetController(IGeoEpithetsDataService service)
            : base(service)
        {
        }
    }
}