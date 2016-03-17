namespace ProcessingTools.Web.Api.Models.GeoEpithet
{
    using Contracts.Mapping;
    using Geo.Services.Data.Models.Contracts;

    public class GeoEpithetResponseModel : IMapFrom<IGeoEpithet>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}