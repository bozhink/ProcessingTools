// <copyright file="IPublishersDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using ProcessingTools.Services.Models.Contracts.Documents.Publishers;

    /// <summary>
    /// Publishers data service.
    /// </summary>
    public interface IPublishersDataService : IDataService<IPublisherModel, IPublisherDetailsModel, IPublisherInsertModel, IPublisherUpdateModel>
    {
    }
}
