﻿// <copyright file="FileInsertModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Files
{
    using System.IO;
    using ProcessingTools.Contracts.Services.Models.Documents.Files;

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
        public string FileExtension => this.OriginalFileExtension;

        /// <inheritdoc/>
        public string FileName => this.OriginalFileName;

        /// <inheritdoc/>
        public long OriginalContentLength { get; set; }

        /// <inheritdoc/>
        public string OriginalContentType { get; set; }

        /// <inheritdoc/>
        public string OriginalFileExtension => Path.GetExtension(this.OriginalFileName);

        /// <inheritdoc/>
        public string OriginalFileName { get; set; }

        /// <inheritdoc/>
        public string SystemFileName { get; set; }
    }
}
