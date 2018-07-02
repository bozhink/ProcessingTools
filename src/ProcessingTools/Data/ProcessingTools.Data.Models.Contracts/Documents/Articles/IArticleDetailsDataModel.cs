// <copyright file="IArticleDetailsDataModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Contracts.Documents.Articles
{
    /// <summary>
    /// Article details data model.
    /// </summary>
    public interface IArticleDetailsDataModel : IArticleDataModel, ProcessingTools.Models.Contracts.Documents.Articles.IArticleDetailsModel
    {
        /// <summary>
        /// Gets the article journal.
        /// </summary>
        IArticleJournalDataModel Journal { get; }
    }
}
