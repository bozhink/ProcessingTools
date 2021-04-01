// <copyright file="DocumentInsertModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Documents
{
    using ProcessingTools.Contracts.Models.Documents.Documents;

    /// <summary>
    /// Document insert model.
    /// </summary>
    public class DocumentInsertModel : Contracts.Services.Models.Documents.Documents.IDocumentInsertModel
    {
        /// <inheritdoc/>
        public string ArticleId { get; set; }

        /// <inheritdoc/>
        public string FileId { get; set; }

        /// <inheritdoc/>
        public IDocumentFileModel File { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }
    }
}
