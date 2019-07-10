// <copyright file="IPublishersDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Documents.Publishers;

namespace ProcessingTools.Contracts.Services.Documents
{
    /// <summary>
    /// Publishers data service.
    /// </summary>
    public interface IPublishersDataService : IDataService<IPublisherModel, IPublisherDetailsModel, IPublisherInsertModel, IPublisherUpdateModel>
    {
    }
}
