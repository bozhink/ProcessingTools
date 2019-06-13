// <copyright file="IFileDetailsDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Documents.Files
{
    using ProcessingTools.Contracts.Models.Documents.Files;

    /// <summary>
    /// File details data transfer object (DTO).
    /// </summary>
    public interface IFileDetailsDataTransferObject : IFileDataTransferObject, IFileDetailsModel
    {
    }
}
