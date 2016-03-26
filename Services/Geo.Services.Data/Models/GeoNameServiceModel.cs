namespace ProcessingTools.Geo.Services.Data.Models
{
    using Contracts;

    public class GeoNameServiceModel : IGeoNameServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}