// <copyright file="IFile.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// File.
    /// </summary>
    public interface IFile : IGuidIdentifiable, ICreated, IModified, IDescribable
    {
        /// <summary>
        /// Gets content length.
        /// </summary>
        long ContentLength { get; }

        /// <summary>
        /// Gets content type.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Gets file extension.
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Gets file name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets full name.
        /// </summary>
        string FullName { get; }
    }
}
