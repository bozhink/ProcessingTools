namespace ProcessingTools.Geo.Services.Data.Entity.Models
{
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    internal class GeoNameModel : IGeoName
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
