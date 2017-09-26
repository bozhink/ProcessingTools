// <copyright file="IFileMetadata.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Files
{
    /// <summary>
    /// File metadata.
    /// </summary>
    public interface IFileMetadata : IIdentifiable, IDescribable, IContentTypeable, IModelWithUserInformation, IFileNameable
    {
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
