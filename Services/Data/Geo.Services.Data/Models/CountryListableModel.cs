namespace ProcessingTools.Geo.Services.Data.Models
{
    using ProcessingTools.Geo.Services.Data.Contracts.Models;

    internal class CountryListableModel : ICountryListableModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
