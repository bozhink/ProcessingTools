namespace ProcessingTools.Web.Api.Models.GeoNames
{
    using Geo.Services.Data.Models.Contracts;
    using Mappings.Contracts;

    public class GeoNameResponseModel : IMapFrom<IGeoNameServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}