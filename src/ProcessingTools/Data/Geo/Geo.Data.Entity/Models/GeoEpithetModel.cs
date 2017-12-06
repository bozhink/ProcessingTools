namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Contracts.Models.Geo;

    internal class GeoEpithetModel : IGeoEpithet
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
