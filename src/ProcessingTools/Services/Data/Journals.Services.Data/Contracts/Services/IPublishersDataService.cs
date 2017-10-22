﻿namespace ProcessingTools.Journals.Services.Data.Contracts.Services
{
    using Models;
    using ProcessingTools.Services.Contracts.Data;

    public interface IPublishersDataService : IAddressableDataService<IPublisherDetails>, IDetailedGenericDataService<IPublisher, IPublisherDetails>
    {
        bool SaveToHistory { get; set; }
    }
}
