// <copyright file="IArticleDetailsModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Documents.Articles
{
    /// <summary>
    /// Article details model.
    /// </summary>
    public interface IArticleDetailsModel : IArticleModel, ProcessingTools.Models.Contracts.Documents.Articles.IArticleDetailsModel
    {
        /// <summary>
        /// Gets or sets the article journal.
        /// </summary>
        IArticleJournalModel Journal { get; set; }
    }
}
