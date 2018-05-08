// <copyright file="FileUpdateModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Files
{
    using ProcessingTools.Services.Models.Contracts.Documents.Files;

    /// <summary>
    /// File update model.
    /// </summary>
    public class FileUpdateModel : IFileUpdateModel
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
    }
}
