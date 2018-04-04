// <copyright file="DocumentInsertModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Documents
{
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Document insert model.
    /// </summary>
    public class DocumentInsertModel : IDocumentInsertModel
    {
        /// <inheritdoc/>
        public string ArticleId { get; set; }

        /// <inheritdoc/>
        public string FileId { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }
    }
}
