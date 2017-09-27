namespace ProcessingTools.Journals.Services.Data.Models.DataModels
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Journals;

    internal class AddressDataModel : IAddress, IDataModel
    {
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }

        public string Id { get; set; }
    }
}
