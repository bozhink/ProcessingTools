namespace ProcessingTools.Web.Api.Controllers
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Api.Models.GeoEpithets;

    public class GeoEpithetController : GenericDataServiceController<IGeoEpithetsDataService, GeoEpithetServiceModel, GeoEpithetRequestModel, GeoEpithetResponseModel, IFilter>
    {
        public GeoEpithetController(IGeoEpithetsDataService service)
            : base(service)
        {
        }
    }
}
