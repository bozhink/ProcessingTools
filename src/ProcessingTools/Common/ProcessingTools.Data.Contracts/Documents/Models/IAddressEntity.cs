namespace ProcessingTools.Contracts.Data.Documents.Models
{
    using ProcessingTools.Models.Contracts;

    public interface IAddressEntity : IGuidIdentifiable, IModelWithUserInformation
    {
        string AddressString { get; }

        int? CityId { get; }

        int? CountryId { get; }
    }
}
