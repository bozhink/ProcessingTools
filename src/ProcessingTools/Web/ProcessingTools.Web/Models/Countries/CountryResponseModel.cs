namespace ProcessingTools.Web.Models.Countries
{
    using ProcessingTools.Contracts.Models;

    public class CountryResponseModel : INameableIntegerIdentifiable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LanguageCode { get; set; }
    }
}
