﻿// <copyright file="FileInsertModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Files
{
    using ProcessingTools.Services.Models.Contracts.Documents.Files;

    /// <summary>
    /// File insert model.
    /// </summary>
    public class FileInsertModel : IFileInsertModel
    {
        /// <inheritdoc/>
        public long ContentLength => this.OriginalContentLength;

        /// <inheritdoc/>
        public string ContentType => this.OriginalContentType;

        /// <inheritdoc/>
        public string FileName => this.OriginalFileName;

        /// <inheritdoc/>
        public long OriginalContentLength { get; set; }

        /// <inheritdoc/>
        public string OriginalContentType { get; set; }

        /// <inheritdoc/>
        public string OriginalFileName { get; set; }

        /// <inheritdoc/>
        public string SystemFileName { get; set; }
    }
}
