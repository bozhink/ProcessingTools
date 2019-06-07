// <copyright file="IFileMetadata.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Files
{
    /// <summary>
    /// File metadata.
    /// </summary>
    public interface IFileMetadata : IIdentifiable, IDescribed, IContentTyped, ICreated, IModified
    {
        /// <summary>
        /// Gets the file name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets content length.
        /// </summary>
        long ContentLength { get; }

        /// <summary>
        /// Gets file extension.
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Gets full name.
        /// </summary>
        string FullName { get; }
    }
}
