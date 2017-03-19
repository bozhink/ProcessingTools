namespace ProcessingTools.Web.Areas.Journals.Models.Publishers
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Journals.Services.Data.Contracts.Models;

    public class Address : IAddress, IServiceModel
    {
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }

        public string Id { get; set; }

        public UpdateStatus Status { get; set; }
    }
}
