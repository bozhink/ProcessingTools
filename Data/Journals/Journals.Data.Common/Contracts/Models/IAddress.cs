namespace ProcessingTools.Journals.Data.Common.Contracts.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IAddress : IStringIdentifiable
    {
        string AddressString { get; }

        int? CityId { get; }

        int? CountryId { get; }
    }
}
