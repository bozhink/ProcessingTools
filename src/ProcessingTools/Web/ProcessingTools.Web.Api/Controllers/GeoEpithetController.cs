namespace ProcessingTools.Web.Api.Controllers
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Api.Models.GeoEpithets;

    public class GeoEpithetController : GenericDataServiceController<IGeoEpithetsDataService, IGeoEpithet, GeoEpithetRequestModel, GeoEpithetResponseModel, ITextFilter>
    {
        public GeoEpithetController(IGeoEpithetsDataService service)
            : base(service)
        {
        }
    }
}
