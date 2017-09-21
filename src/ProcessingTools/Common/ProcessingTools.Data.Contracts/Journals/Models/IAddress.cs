namespace ProcessingTools.Contracts.Data.Journals.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IAddress : IStringIdentifiable, ProcessingTools.Contracts.Models.IAddressable, IDataModel
    {
        int? CityId { get; }

        int? CountryId { get; }
    }
}
