﻿// <copyright file="IDocument.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Services.Data.Documents
{
    using System;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Document service model.
    /// </summary>
    public interface IDocument : IStringIdentifiable, IContentTyped, ICommentable
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
