// <copyright file="DocumentInsertModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Documents
{
    using ProcessingTools.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Document insert model.
    /// </summary>
    public class DocumentInsertModel : ProcessingTools.Services.Models.Contracts.Documents.Documents.IDocumentInsertModel
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
