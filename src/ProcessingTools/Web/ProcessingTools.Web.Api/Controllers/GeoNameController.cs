namespace ProcessingTools.Web.Api.Controllers
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Api.Models.GeoNames;

    public class GeoNameController : GenericDataServiceController<IGeoNamesDataService, IGeoName, GeoNameRequestModel, GeoNameResponseModel, ITextFilter>
    {
        public GeoNameController(IGeoNamesDataService service)
            : base(service)
        {
        }
    }
}
