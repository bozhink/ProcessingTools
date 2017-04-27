namespace ProcessingTools.Web.Api.Controllers
{
    using Abstractions;
    using Geo.Services.Data.Contracts;
    using Geo.Services.Data.Models;
    using Models.GeoEpithets;

    public class GeoEpithetController : GenericDataServiceController<GeoEpithetServiceModel, GeoEpithetRequestModel, GeoEpithetResponseModel>
    {
        public GeoEpithetController(IGeoEpithetsDataService service)
            : base(service)
        {
        }
    }
}