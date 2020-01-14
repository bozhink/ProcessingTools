// <copyright file="IPublisherDetailsDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Documents.Publishers
{
    using ProcessingTools.Contracts.Models.Documents.Publishers;

    /// <summary>
    /// Publisher details data transfer object (DTO).
    /// </summary>
    public interface IPublisherDetailsDataTransferObject : IPublisherDataTransferObject, IPublisherDetailsModel
    {
    }
}
