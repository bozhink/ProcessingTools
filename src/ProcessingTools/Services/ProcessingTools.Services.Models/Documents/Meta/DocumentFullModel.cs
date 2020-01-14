// <copyright file="DocumentFullModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Meta
{
    using ProcessingTools.Contracts.Models.Documents.Articles;
    using ProcessingTools.Contracts.Models.Documents.Documents;
    using ProcessingTools.Contracts.Models.Documents.Journals;
    using ProcessingTools.Contracts.Models.Documents.Meta;
    using ProcessingTools.Contracts.Models.Documents.Publishers;

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
