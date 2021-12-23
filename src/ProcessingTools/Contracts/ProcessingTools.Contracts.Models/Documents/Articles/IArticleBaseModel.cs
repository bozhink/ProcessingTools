// <copyright file="IArticleBaseModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Articles
{
    /// <summary>
    /// Article base model.
    /// </summary>
    public interface IArticleBaseModel : IArticleMetaModel
    {
        /// <summary>
        /// Gets the journal ID.
        /// </summary>
        string JournalId { get; }

        /// <summary>
        /// Gets the number of pages.
        /// </summary>
        int NumberOfPages { get; }
    }
}
