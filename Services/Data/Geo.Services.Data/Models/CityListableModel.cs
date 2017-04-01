namespace ProcessingTools.Geo.Services.Data.Models
{
    using ProcessingTools.Geo.Services.Data.Contracts.Models;

    internal class CityListableModel : ICityListableModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
