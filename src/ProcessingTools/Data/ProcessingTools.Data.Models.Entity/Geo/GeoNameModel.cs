namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    public class GeoNameModel : IGeoName
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
