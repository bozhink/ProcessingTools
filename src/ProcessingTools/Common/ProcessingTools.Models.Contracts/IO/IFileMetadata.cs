// <copyright file="IFileMetadata.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.IO
{
    /// <summary>
    /// File metadata.
    /// </summary>
    public interface IFileMetadata
    {
        /// <summary>
        /// Gets the content type.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Gets the content length.
        /// </summary>
        long ContentLength { get; }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        string FileName { get; }
    }
}
