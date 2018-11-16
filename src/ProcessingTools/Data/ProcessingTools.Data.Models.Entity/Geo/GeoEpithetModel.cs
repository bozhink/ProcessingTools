namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    public class GeoEpithetModel : IGeoEpithet
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
