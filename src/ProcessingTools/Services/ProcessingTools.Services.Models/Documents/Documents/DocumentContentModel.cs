// <copyright file="DocumentContentModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Documents
{
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Document content model.
    /// </summary>
    public class DocumentContentModel : IDocumentContentModel
    {
        /// <inheritdoc/>
        public string DocumentId { get; set; }

        /// <inheritdoc/>
        public string Content { get; set; }

        /// <inheritdoc/>
        public string ContentType { get; set; }
    }
}
