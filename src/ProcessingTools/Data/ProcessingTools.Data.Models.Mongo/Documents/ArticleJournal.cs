// <copyright file="ArticleJournal.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Documents
{
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Articles;

    /// <summary>
    /// Article journal.
    /// </summary>
    public class ArticleJournal : IArticleJournalDataTransferObject
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string AbbreviatedName { get; set; }
    }
}
