// <copyright file="IDocumentFileStreamModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Documents.Documents
{
    using System.IO;
    using ProcessingTools.Models.Contracts.IO;

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
