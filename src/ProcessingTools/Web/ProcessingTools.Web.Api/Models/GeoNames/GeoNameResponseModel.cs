namespace ProcessingTools.Web.Api.Models.GeoNames
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Geo.Services.Data.Models;

    public class GeoNameResponseModel : IMapFrom<GeoNameServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
