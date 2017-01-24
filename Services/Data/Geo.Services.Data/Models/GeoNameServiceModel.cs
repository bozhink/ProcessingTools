namespace ProcessingTools.Geo.Services.Data.Models
{
    using ProcessingTools.Contracts.Models;

    public class GeoNameServiceModel : INameableIntegerIdentifiable
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
