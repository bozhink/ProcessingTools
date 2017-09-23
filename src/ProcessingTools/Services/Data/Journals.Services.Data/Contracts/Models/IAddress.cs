namespace ProcessingTools.Journals.Services.Data.Contracts.Models
{
    using ProcessingTools.Models.Contracts;

    public interface IAddress : IStringIdentifiable, ProcessingTools.Models.Contracts.IAddressable, IServiceModel
    {
        int? CityId { get; }

        int? CountryId { get; }
    }
}
