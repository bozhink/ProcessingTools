namespace ProcessingTools.Web.Api.Models.GeoEpithets
{
    using Contracts.Mapping;
    using Geo.Services.Data.Models.Contracts;

    public class GeoEpithetResponseModel : IMapFrom<IGeoEpithetServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}