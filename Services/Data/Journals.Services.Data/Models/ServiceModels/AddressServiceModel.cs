namespace ProcessingTools.Journals.Services.Data.Models.ServiceModels
{
    using Contracts.Models;
    using ProcessingTools.Contracts.Models;

    internal class AddressServiceModel : IAddress, IServiceModel
    {
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }

        public string Id { get; set; }
    }
}
