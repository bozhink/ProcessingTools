namespace ProcessingTools.Documents.Data.Common.Contracts.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IAddressEntity : IGuidIdentifiable, IModelWithUserInformation
    {
        string AddressString { get; }

        int? CityId { get; }

        int? CountryId { get; }
    }
}
