namespace ProcessingTools.Journals.Services.Data.Models.ServiceModels
{
    using Contracts.Models;

    public class Address : IAddress
    {
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }

        public string Id { get; set; }
    }
}
