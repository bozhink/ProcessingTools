// <copyright file="IArticleFullModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Meta
{
    using ProcessingTools.Contracts.Models.Documents.Articles;
    using ProcessingTools.Contracts.Models.Documents.Journals;
    using ProcessingTools.Contracts.Models.Documents.Publishers;

    /// <summary>
    /// Article full model.
    /// </summary>
    public interface IArticleFullModel
    {
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
