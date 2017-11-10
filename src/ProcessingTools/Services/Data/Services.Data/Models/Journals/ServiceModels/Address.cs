namespace ProcessingTools.Journals.Services.Data.Models.ServiceModels
{
    using ProcessingTools.Services.Models.Contracts.Data.Journals;

    public class Address : IAddress
    {
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }

        public string Id { get; set; }
    }
}
