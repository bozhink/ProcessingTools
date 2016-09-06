namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using ProcessingTools.Contracts;

    public interface IAddressEntity : IGuidIdentifiable, IModelWithUserInformation
    {
        string AddressString { get; }

        int? CityId { get; }

        int? CountryId { get; }
    }
}
