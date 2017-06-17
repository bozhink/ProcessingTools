namespace ProcessingTools.Web.Api.Controllers
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Api.Models.GeoNames;

    public class GeoNameController : GenericDataServiceController<IGeoNamesDataService, GeoNameServiceModel, GeoNameRequestModel, GeoNameResponseModel, IFilter>
    {
        public GeoNameController(IGeoNamesDataService service)
            : base(service)
        {
        }
    }
}
