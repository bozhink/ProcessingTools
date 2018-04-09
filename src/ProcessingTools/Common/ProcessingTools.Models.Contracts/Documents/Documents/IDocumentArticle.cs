// <copyright file="IDocumentArticle.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Documents
{
    /// <summary>
    /// Document article.
    /// </summary>
    public interface IDocumentArticle
    {
        /// <summary>
        /// Gets the object ID of the article.
        /// </summary>
        string ArticleId { get; }

        /// <summary>
        /// Gets the title of the article.
        /// </summary>
        string ArticleTitle { get; }

        /// <summary>
        /// Gets the object ID of the journal.
        /// </summary>
        string JournalId { get; }

        /// <summary>
        /// Gets the name of the journal.
        /// </summary>
        string JournalName { get; }
    }
}
