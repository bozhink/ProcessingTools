// <copyright file="DocumentFileModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Documents
{
    /// <summary>
    /// Document file model.
    /// </summary>
    public class DocumentFileModel : ProcessingTools.Services.Models.Contracts.Documents.Documents.IDocumentFileModel
    {
        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public long ContentLength { get; set; }

        /// <inheritdoc/>
        public string FileExtension { get; set; }

        /// <inheritdoc/>
        public string FileName { get; set; }
    }
}
