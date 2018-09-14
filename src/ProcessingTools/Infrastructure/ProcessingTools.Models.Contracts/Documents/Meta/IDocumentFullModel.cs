// <copyright file="IDocumentFullModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Meta
{
    using ProcessingTools.Models.Contracts.Documents.Articles;
    using ProcessingTools.Models.Contracts.Documents.Documents;
    using ProcessingTools.Models.Contracts.Documents.Journals;
    using ProcessingTools.Models.Contracts.Documents.Publishers;

    /// <summary>
    /// Document full model.
    /// </summary>
    public interface IDocumentFullModel
    {
        /// <summary>
        /// Gets the document.
        /// </summary>
        IDocumentModel Document { get; }

        /// <summary>
        /// Gets the article.
        /// </summary>
        IArticleModel Article { get; }

        /// <summary>
        /// Gets the journal.
        /// </summary>
        IJournalModel Journal { get; }

        /// <summary>
        /// Gets the publisher.
        /// </summary>
        IPublisherModel Publisher { get; }
    }
}
