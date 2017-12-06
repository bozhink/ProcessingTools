namespace ProcessingTools.Journals.Services.Data.Contracts.Services
{
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Contracts.Services.Models.Data.Journals;

    public interface IPublishersDataService : IAddressableDataService<IPublisherDetails>, IDetailedGenericDataService<IPublisher, IPublisherDetails>
    {
        bool SaveToHistory { get; set; }
    }
}
