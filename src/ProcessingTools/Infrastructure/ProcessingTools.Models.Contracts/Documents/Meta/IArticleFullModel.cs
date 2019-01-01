﻿// <copyright file="IArticleFullModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Meta
{
    using ProcessingTools.Models.Contracts.Documents.Articles;
    using ProcessingTools.Models.Contracts.Documents.Journals;
    using ProcessingTools.Models.Contracts.Documents.Publishers;

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
