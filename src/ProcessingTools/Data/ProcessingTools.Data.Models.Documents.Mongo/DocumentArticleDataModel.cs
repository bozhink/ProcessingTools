// <copyright file="DocumentArticleDataModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Documents.Mongo
{
    using ProcessingTools.Data.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Document article data model.
    /// </summary>
    public class DocumentArticleDataModel : IDocumentArticleDataModel
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
