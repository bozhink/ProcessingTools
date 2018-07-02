// <copyright file="DocumentFileStreamModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Documents
{
    using System.IO;
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Document file stream model.
    /// </summary>
    public class DocumentFileStreamModel : IDocumentFileStreamModel
    {
        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public long ContentLength { get; set; }

        /// <inheritdoc/>
        public string FileExtension { get; set; }

        /// <inheritdoc/>
        public string FileName { get; set; }

        /// <inheritdoc/>
        public Stream Stream { get; set; }
    }
}
