namespace ProcessingTools.Geo.Services.Data.Models.Countries
{
    using Contracts;

    public class CountryListableServiceModel : ICountryListableServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
