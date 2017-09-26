// <copyright file="IDocument.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Documents.Models
{
    using ProcessingTools.Models.Contracts;
    using System;

    /// <summary>
    /// Document.
    /// </summary>
    // TODO: separation with IFileEntity
    public interface IDocument : IGuidIdentifiable, ICommentable, IModelWithUserInformation
    {
        /// <summary>
        /// Gets article IS.
        /// </summary>
        Guid ArticleId { get; }

        //// TODO: add
        //// Guid FileId { get; }

        //// TODO: add
        //// string Description { get; }

        /// <summary>
        /// Gets file path
        /// </summary>
        // TODO: remove
        string FilePath { get; }

        /// <summary>
        /// Gets content length.
        /// </summary>
        // TODO: remove
        long ContentLength { get; }

        /// <summary>
        /// Gets content type.
        /// </summary>
        // TODO: remove
        string ContentType { get; }

        /// <summary>
        /// Gets file extension.
        /// </summary>
        // TODO: remove
        string FileExtension { get; }

        /// <summary>
        /// Gets file name.
        /// </summary>
        // TODO: remove
        string FileName { get; }

        /// <summary>
        /// Gets original content length.
        /// </summary>
        // TODO: remove
        long OriginalContentLength { get; }

        /// <summary>
        /// Gets original content type.
        /// </summary>
        // TODO: remove
        string OriginalContentType { get; }

        /// <summary>
        /// Gets original file extension.
        /// </summary>
        // TODO: remove
        string OriginalFileExtension { get; }

        /// <summary>
        /// Gets original file name.
        /// </summary>
        // TODO: remove
        string OriginalFileName { get; }
    }
}
