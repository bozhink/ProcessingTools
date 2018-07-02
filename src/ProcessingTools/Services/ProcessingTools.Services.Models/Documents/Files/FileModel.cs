// <copyright file="FileModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Files
{
    using System;
    using ProcessingTools.Services.Models.Contracts.Documents.Files;

    /// <summary>
    /// File model.
    /// </summary>
    public class FileModel : IFileModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public long ContentLength { get; set; }

        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public string FileExtension { get; set; }

        /// <inheritdoc/>
        public string FileName { get; set; }

        /// <inheritdoc/>
        public long OriginalContentLength { get; set; }

        /// <inheritdoc/>
        public string OriginalContentType { get; set; }

        /// <inheritdoc/>
        public string OriginalFileExtension { get; set; }

        /// <inheritdoc/>
        public string OriginalFileName { get; set; }

        /// <inheritdoc/>
        public string SystemFileName { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }
    }
}
