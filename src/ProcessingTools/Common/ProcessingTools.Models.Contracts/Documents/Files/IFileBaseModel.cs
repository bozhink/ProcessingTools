// <copyright file="IFileBaseModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Files
{
    using ProcessingTools.Models.Contracts.IO;

    /// <summary>
    /// File base model.
    /// </summary>
    public interface IFileBaseModel : IFileMetadata, IOriginalFileMetadata, ISystemFileMetadata
    {
    }
}
