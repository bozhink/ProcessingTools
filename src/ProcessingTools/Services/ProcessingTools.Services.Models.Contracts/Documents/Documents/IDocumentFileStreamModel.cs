// <copyright file="IDocumentFileStreamModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.IO;
using ProcessingTools.Contracts.Models.IO;

namespace ProcessingTools.Contracts.Services.Models.Documents.Documents
{
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
