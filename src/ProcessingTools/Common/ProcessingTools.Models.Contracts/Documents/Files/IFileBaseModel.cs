// <copyright file="IFileBaseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Files
{
    /// <summary>
    /// File base model.
    /// </summary>
    public interface IFileBaseModel : IContentTyped
    {
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

        /// <summary>
        /// Gets the original content length.
        /// </summary>
        long OriginalContentLength { get; }

        /// <summary>
        /// Gets the original content type.
        /// </summary>
        string OriginalContentType { get; }

        /// <summary>
        /// Gets the original file extension.
        /// </summary>
        string OriginalFileExtension { get; }

        /// <summary>
        /// Gets the original file name.
        /// </summary>
        string OriginalFileName { get; }

        /// <summary>
        /// Gets the system file name.
        /// </summary>
        string SystemFileName { get; }
    }
}
