namespace ProcessingTools.Journals.Services.Data.Contracts.Models
{
    public interface IAddress : ProcessingTools.Contracts.Models.IStringIdentifiable, ProcessingTools.Contracts.Models.IAddressable
    {
        int? CityId { get; }

        string City { get; }

        int? CountryId { get; }

        string Country { get; }
    }
}
