// <copyright file="DocumentArticle.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Documents
{
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Document article.
    /// </summary>
    public class DocumentArticle : IDocumentArticle
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
