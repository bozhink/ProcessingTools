// <copyright file="IArticleDetailsDataModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Documents.Articles
{
    /// <summary>
    /// Article details data model.
    /// </summary>
    public interface IArticleDetailsDataModel : IArticleDataModel, ProcessingTools.Contracts.Models.Documents.Articles.IArticleDetailsModel
    {
        /// <summary>
        /// Gets the article journal.
        /// </summary>
        IArticleJournalDataModel Journal { get; }
    }
}
