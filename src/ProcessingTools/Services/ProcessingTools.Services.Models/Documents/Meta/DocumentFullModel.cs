// <copyright file="DocumentFullModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Meta
{
    using ProcessingTools.Models.Contracts.Documents.Articles;
    using ProcessingTools.Models.Contracts.Documents.Documents;
    using ProcessingTools.Models.Contracts.Documents.Journals;
    using ProcessingTools.Models.Contracts.Documents.Meta;
    using ProcessingTools.Models.Contracts.Documents.Publishers;

    /// <summary>
    /// Document full model.
    /// </summary>
    public class DocumentFullModel : IDocumentFullModel
    {
        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        public IDocumentModel Document { get; set; }

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
    }
}
