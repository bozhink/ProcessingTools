// <copyright file="ArticleJournal.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Documents
{
    using Contracts.Documents.Articles;

    /// <summary>
    /// Article journal
    /// </summary>
    public class ArticleJournal : IArticleJournalDataModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string AbbreviatedName { get; set; }
    }
}
