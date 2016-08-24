namespace ProcessingTools.Documents.Services.Data.Models.Publishers.Contracts
{
    using ProcessingTools.Contracts;

    public interface IPublisherAddress : IGuidIdentifiable
    {
        string AddressString { get; }

        int? CityId { get; }

        int? CountryId { get; }
    }
}
