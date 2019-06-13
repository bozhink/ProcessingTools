﻿// <copyright file="IDocumentDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Documents.Documents
{
    using ProcessingTools.Contracts.Models.Documents.Documents;

    /// <summary>
    /// Document data transfer object (DTO).
    /// </summary>
    public interface IDocumentDataTransferObject : IDataTransferObject, IDocumentModel
    {
    }
}