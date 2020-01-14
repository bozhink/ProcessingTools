// <copyright file="IDocumentFileStreamModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Models.Documents.Documents
{
    using System.IO;
    using ProcessingTools.Contracts.Models.IO;

    /// <summary>
    /// Document file stream model.
    /// </summary>
    public interface IDocumentFileStreamModel : IFileMetadata
    {
        /// <summary>
        /// Gets the content stream.
        /// </summary>
        Stream Stream { get; }
    }
}
