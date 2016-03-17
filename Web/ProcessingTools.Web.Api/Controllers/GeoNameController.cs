namespace ProcessingTools.Web.Api.Controllers
{
    using Factories;
    using Geo.Services.Data.Contracts;
    using Geo.Services.Data.Models.Contracts;
    using Models.GeoName;

    public class GeoNameController : GenericDataServiceControllerFactory<IGeoName, GeoNameRequestModel, GeoNameResponseModel>
    {
        public GeoNameController(IGeoNamesDataService service)
            : base(service)
        {
        }
    }
}