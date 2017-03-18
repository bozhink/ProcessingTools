namespace ProcessingTools.Journals.Services.Data.Models.DataModels
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Journals.Data.Common.Contracts.Models;

    internal class AddressDataModel : IAddress, IDataModel
    {
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }

        public string Id { get; set; }
    }
}
