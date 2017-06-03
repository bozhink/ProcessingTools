namespace ProcessingTools.Web.Api.Models.GeoEpithets
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Geo.Services.Data.Models;

    public class GeoEpithetResponseModel : IMapFrom<GeoEpithetServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
