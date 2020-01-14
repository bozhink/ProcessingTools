// <copyright file="IPublishersDataService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Documents
{
    using ProcessingTools.Contracts.Services.Models.Documents.Publishers;

    /// <summary>
    /// Publishers data service.
    /// </summary>
    public interface IPublishersDataService : IDataService<IPublisherModel, IPublisherDetailsModel, IPublisherInsertModel, IPublisherUpdateModel>
    {
    }
}
