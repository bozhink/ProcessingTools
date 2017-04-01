namespace ProcessingTools.Web.Models.Cities
{
    using ProcessingTools.Contracts.Models;

    public class CityResponseModel : INameableIntegerIdentifiable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CountryResponseModel Country { get; set; }
    }
}
