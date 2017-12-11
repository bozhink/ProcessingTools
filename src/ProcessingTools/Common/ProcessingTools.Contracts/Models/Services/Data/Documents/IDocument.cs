// <copyright file="IDocument.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Services.Data.Documents
{
    using System;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document service model.
    /// </summary>
    public interface IDocument : IStringIdentifiable, IContentTypeable, ICommentable
    {
        /// <summary>
        /// Gets file content length.
        /// </summary>
        long ContentLength { get; }

        /// <summary>
        /// Gets date created.
        /// </summary>
        DateTime DateCreated { get; }

        /// <summary>
        /// Gets date modified.
        /// </summary>
        DateTime DateModified { get; }

        /// <summary>
        /// Gets file extension.
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Gets file name.
        /// </summary>
        string FileName { get; }
    }
}
