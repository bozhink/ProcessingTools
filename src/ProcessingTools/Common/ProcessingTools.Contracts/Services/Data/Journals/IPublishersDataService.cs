// <copyright file="IPublishersDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Journals.Services.Data.Contracts.Services
{
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Contracts.Models.Services.Data.Journals;

    public interface IPublishersDataService : IAddressableDataService<IPublisherDetails>, IDetailedGenericDataService<IPublisher, IPublisherDetails>
    {
        bool SaveToHistory { get; set; }
    }
}
