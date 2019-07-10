// <copyright file="DocumentFileStreamModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Documents.Documents;

namespace ProcessingTools.Services.Models.Documents.Documents
{
    using System.IO;

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
