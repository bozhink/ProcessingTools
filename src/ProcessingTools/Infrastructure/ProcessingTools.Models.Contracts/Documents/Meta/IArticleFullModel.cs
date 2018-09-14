// <copyright file="IArticleFullModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Meta
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Documents.Articles;
    using ProcessingTools.Models.Contracts.Documents.Documents;
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

        /// <summary>
        /// Gets documents of the article.
        /// </summary>
        IEnumerable<IDocumentModel> Documents { get; }
    }
}
