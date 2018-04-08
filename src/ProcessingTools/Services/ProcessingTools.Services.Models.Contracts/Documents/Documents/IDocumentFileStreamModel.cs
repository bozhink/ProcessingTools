// <copyright file="IDocumentFileStreamModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Documents.Documents
{
    using System.IO;

    /// <summary>
    /// Document file stream model.
    /// </summary>
    public interface IDocumentFileStreamModel
    {
        /// <summary>
        /// Gets the content type of the file.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Gets the content length of the file.
        /// </summary>
        long ContentLength { get; }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets the content stream.
        /// </summary>
        Stream Stream { get; }
    }
}
