namespace ProcessingTools.Journals.Data.Common.Contracts.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IAddress : IGuidIdentifiable, IModelWithUserInformation
    {
        string AddressString { get; }

        int? CityId { get; }

        int? CountryId { get; }
    }
}
