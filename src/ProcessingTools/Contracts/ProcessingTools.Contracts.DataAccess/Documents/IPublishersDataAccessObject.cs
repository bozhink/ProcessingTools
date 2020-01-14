// <copyright file="IPublishersDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Documents
{
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Publishers;
    using ProcessingTools.Contracts.Models.Documents.Publishers;

    /// <summary>
    /// Publishers data access object.
    /// </summary>
    public interface IPublishersDataAccessObject : IDataAccessObject<IPublisherDataTransferObject, IPublisherDetailsDataTransferObject, IPublisherInsertModel, IPublisherUpdateModel>
    {
    }
}
