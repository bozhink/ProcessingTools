namespace ProcessingTools.Contracts.Data.Journals.Models
{
    using ProcessingTools.Models.Contracts;

    public interface IAddress : IStringIdentifiable, ProcessingTools.Models.Contracts.IAddressable, IDataModel
    {
        int? CityId { get; }

        int? CountryId { get; }
    }
}
