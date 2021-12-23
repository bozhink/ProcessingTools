// <copyright file="DocumentArticleModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Documents
{
    using ProcessingTools.Contracts.Services.Models.Documents.Documents;

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
