namespace ProcessingTools.Geo.Services.Data.Models
{
    using Contracts;

    public class GeoEpithetResponseModel : IGeoEpithet
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}