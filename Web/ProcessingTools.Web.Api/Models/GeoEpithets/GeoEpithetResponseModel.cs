namespace ProcessingTools.Web.Api.Models.GeoEpithets
{
    using Geo.Services.Data.Models;
    using Mappings.Contracts;

    public class GeoEpithetResponseModel : IMapFrom<GeoEpithetServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}