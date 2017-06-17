namespace ProcessingTools.Geo.Services.Data.Entity.Models
{
    using ProcessingTools.Contracts.Models.Geo;

    internal class GeoNameModel : IGeoName
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
