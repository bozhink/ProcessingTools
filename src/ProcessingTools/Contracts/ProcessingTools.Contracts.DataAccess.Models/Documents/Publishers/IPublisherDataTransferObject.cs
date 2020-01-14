// <copyright file="IPublisherDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Documents.Publishers
{
    using ProcessingTools.Contracts.Models.Documents.Publishers;

    /// <summary>
    /// Publisher data transfer object (DTO).
    /// </summary>
    public interface IPublisherDataTransferObject : IDataTransferObject, IPublisherModel
    {
    }
}
