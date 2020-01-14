// <copyright file="IDocumentDetailsDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Documents.Documents
{
    using ProcessingTools.Contracts.Models.Documents.Documents;

    /// <summary>
    /// Document details data transfer object (DTO).
    /// </summary>
    public interface IDocumentDetailsDataTransferObject : IDocumentDataTransferObject, IDocumentDetailsModel
    {
    }
}
