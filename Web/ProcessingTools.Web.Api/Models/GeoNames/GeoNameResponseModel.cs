namespace ProcessingTools.Web.Api.Models.GeoNames
{
    using Geo.Services.Data.Models;
    using Mappings.Contracts;

    public class GeoNameResponseModel : IMapFrom<GeoNameServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}