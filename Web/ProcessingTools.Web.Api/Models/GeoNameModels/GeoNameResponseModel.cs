namespace ProcessingTools.Web.Api.Models.GeoNameModels
{
    using Contracts.Mapping;
    using Geo.Services.Data.Models.Contracts;

    public class GeoNameResponseModel : IMapFrom<IGeoName>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}