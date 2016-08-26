namespace ProcessingTools.Geo.Services.Data.Models.Cities
{
    using Contracts;

    public class CityListableServiceModel : ICityListableServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
