// <copyright file="IFileBaseModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Files
{
    using ProcessingTools.Contracts.Models.IO;

    /// <summary>
    /// File base model.
    /// </summary>
    public interface IFileBaseModel : IFileMetadata, IOriginalFileMetadata, ISystemFileMetadata
    {
    }
}
