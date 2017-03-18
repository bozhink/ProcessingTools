namespace ProcessingTools.Journals.Services.Data.Contracts.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IAddress : IStringIdentifiable, ProcessingTools.Contracts.Models.IAddressable, IServiceModel
    {
        int? CityId { get; }

        string City { get; }

        int? CountryId { get; }

        string Country { get; }
    }
}
