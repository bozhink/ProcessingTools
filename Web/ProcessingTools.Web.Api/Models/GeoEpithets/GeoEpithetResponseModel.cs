namespace ProcessingTools.Web.Api.Models.GeoEpithets
{
    using Geo.Services.Data.Models.Contracts;
    using Mappings.Contracts;

    public class GeoEpithetResponseModel : IMapFrom<IGeoEpithetServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}