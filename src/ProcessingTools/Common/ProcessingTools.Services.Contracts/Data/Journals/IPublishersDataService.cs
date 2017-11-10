namespace ProcessingTools.Journals.Services.Data.Contracts.Services
{
    using ProcessingTools.Services.Contracts.Data;
    using ProcessingTools.Services.Models.Contracts.Data.Journals;

    public interface IPublishersDataService : IAddressableDataService<IPublisherDetails>, IDetailedGenericDataService<IPublisher, IPublisherDetails>
    {
        bool SaveToHistory { get; set; }
    }
}
