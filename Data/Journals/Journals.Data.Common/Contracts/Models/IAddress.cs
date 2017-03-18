namespace ProcessingTools.Journals.Data.Common.Contracts.Models
{
    public interface IAddress : ProcessingTools.Contracts.Models.IStringIdentifiable, ProcessingTools.Contracts.Models.IAddressable
    {
        int? CityId { get; }

        int? CountryId { get; }
    }
}
