// <copyright file="IPublishersDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Journals
{
    using ProcessingTools.Models.Contracts.Services.Data.Journals;

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
