namespace ProcessingTools.Web.Api.Controllers
{
    using Abstractions;
    using Geo.Services.Data.Contracts;
    using Geo.Services.Data.Models;
    using Models.GeoNames;

    public class GeoNameController : GenericDataServiceController<GeoNameServiceModel, GeoNameRequestModel, GeoNameResponseModel>
    {
        public GeoNameController(IGeoNamesDataService service)
            : base(service)
        {
        }
    }
}
