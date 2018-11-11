// <copyright file="IOriginalFileMetadata.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.IO
{
    /// <summary>
    /// Original file metadata.
    /// </summary>
    public interface IOriginalFileMetadata
    {
        /// <summary>
        /// Gets the original content type.
        /// </summary>
        string OriginalContentType { get; }

        /// <summary>
        /// Gets the original content length.
        /// </summary>
        long OriginalContentLength { get; }

        /// <summary>
        /// Gets the original file extension.
        /// </summary>
        string OriginalFileExtension { get; }

        /// <summary>
        /// Gets the original file name.
        /// </summary>
        string OriginalFileName { get; }
    }
}
