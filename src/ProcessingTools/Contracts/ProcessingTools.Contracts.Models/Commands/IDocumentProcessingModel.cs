// <copyright file="IDocumentProcessingModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Commands
{
    /// <summary>
    /// Document processing model.
    /// </summary>
    public interface IDocumentProcessingModel
    {
        /// <summary>
        /// Gets the ID of the document.
        /// </summary>
        string DocumentId { get; }

        /// <summary>
        /// Gets the ID of the article.
        /// </summary>
        string ArticleId { get; }

        /// <summary>
        /// Gets the ID of the journal style.
        /// </summary>
        string JournalStyleId { get; }
    }
}
