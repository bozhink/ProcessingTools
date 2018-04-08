// <copyright file="DocumentFileModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Documents
{
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Document file model.
    /// </summary>
    public class DocumentFileModel : IDocumentFileModel
    {
        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public string ContentDisposition { get; set; }

        /// <inheritdoc/>
        public long Length { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string FileName { get; set; }
    }
}
