// <copyright file="ArticleFullModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Meta
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Documents.Articles;
    using ProcessingTools.Models.Contracts.Documents.Documents;
    using ProcessingTools.Models.Contracts.Documents.Journals;
    using ProcessingTools.Models.Contracts.Documents.Meta;
    using ProcessingTools.Models.Contracts.Documents.Publishers;

    /// <summary>
    /// Article full model.
    /// </summary>
    public class ArticleFullModel : IArticleFullModel
    {
        /// <summary>
        /// Gets or sets the article.
        /// </summary>
        public IArticleModel Article { get; set; }

        /// <summary>
        /// Gets or sets the journal.
        /// </summary>
        public IJournalModel Journal { get; set; }

        /// <summary>
        /// Gets or sets the publisher.
        /// </summary>
        public IPublisherModel Publisher { get; set; }

        /// <summary>
        /// Gets or sets the documents of the article.
        /// </summary>
        public IEnumerable<IDocumentModel> Documents { get; set; }
    }
}
