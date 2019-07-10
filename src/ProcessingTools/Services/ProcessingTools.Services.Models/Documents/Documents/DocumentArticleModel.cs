// <copyright file="DocumentArticleModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Documents.Documents;

namespace ProcessingTools.Services.Models.Documents.Documents
{
    /// <summary>
    /// Document article.
    /// </summary>
    public class DocumentArticleModel : IDocumentArticleModel
    {
        /// <inheritdoc/>
        public string ArticleId { get; set; }

        /// <inheritdoc/>
        public string ArticleTitle { get; set; }

        /// <inheritdoc/>
        public string JournalId { get; set; }

        /// <inheritdoc/>
        public string JournalName { get; set; }
    }
}
