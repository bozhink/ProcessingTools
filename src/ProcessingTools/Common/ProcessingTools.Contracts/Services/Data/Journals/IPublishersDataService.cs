// <copyright file="IPublishersDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Journals
{
    using ProcessingTools.Contracts.Models.Services.Data.Journals;
    using ProcessingTools.Contracts.Services.Data;

    /// <summary>
    /// Publishers data service.
    /// </summary>
    public interface IPublishersDataService : IAddressableDataService<IPublisherDetails>, IDetailedGenericDataService<IPublisher, IPublisherDetails>
    {
        /// <summary>
        /// Gets or sets a value indicating whether entities have to be added to history.
        /// </summary>
        bool SaveToHistory { get; set; }
    }
}
