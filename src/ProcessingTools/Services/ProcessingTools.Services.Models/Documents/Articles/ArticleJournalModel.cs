// <copyright file="ArticleJournalModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Articles
{
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;

    /// <summary>
    /// Article journal model.
    /// </summary>
    public class ArticleJournalModel : IArticleJournalModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string AbbreviatedName { get; set; }
    }
}
