namespace ProcessingTools.Journals.Services.Data.Contracts.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IAddress : IStringIdentifiable, ProcessingTools.Contracts.Models.IAddressable, IServiceModel
    {
        int? CityId { get; }

        int? CountryId { get; }
    }
}
