namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Models.Contracts.Geo;

    internal class GeoNameModel : IGeoName
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
