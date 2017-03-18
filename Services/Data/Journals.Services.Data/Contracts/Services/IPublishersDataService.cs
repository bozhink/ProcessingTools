namespace ProcessingTools.Journals.Services.Data.Contracts.Services
{
    using Models;
    using ProcessingTools.Contracts.Services.Data;

    public interface IPublishersDataService : IAddressableDataService<IPublisherDetails>, IDetailedGenericDataService<IPublisher, IPublisherDetails>
    {
    }
}
