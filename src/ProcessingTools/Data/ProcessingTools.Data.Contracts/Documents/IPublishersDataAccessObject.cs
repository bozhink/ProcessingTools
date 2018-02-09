// <copyright file="IPublishersDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Documents
{
    using ProcessingTools.Data.Models.Contracts.Documents.Publishers;
    using ProcessingTools.Models.Contracts.Documents.Publishers;

    /// <summary>
    /// Publishers data access object.
    /// </summary>
    public interface IPublishersDataAccessObject : IDataAccessObject<IPublisherDataModel, IPublisherDetailsDataModel, IPublisherInsertModel, IPublisherUpdateModel>
    {
    }
}
