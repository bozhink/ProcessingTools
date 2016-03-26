namespace ProcessingTools.Web.Api.Controllers
{
    using Factories;
    using Geo.Services.Data.Contracts;
    using Geo.Services.Data.Models.Contracts;
    using Models.GeoNames;

    public class GeoNameController : GenericDataServiceControllerFactory<IGeoNameServiceModel, GeoNameRequestModel, GeoNameResponseModel>
    {
        public GeoNameController(IGeoNamesDataService service)
            : base(service)
        {
        }
    }
}