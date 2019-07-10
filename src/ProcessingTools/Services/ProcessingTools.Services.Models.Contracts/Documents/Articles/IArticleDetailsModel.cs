// <copyright file="IArticleDetailsModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Models.Documents.Articles
{
    /// <summary>
    /// Article details model.
    /// </summary>
    public interface IArticleDetailsModel : IArticleModel, ProcessingTools.Contracts.Models.Documents.Articles.IArticleDetailsModel
    {
        /// <summary>
        /// Gets or sets the article journal.
        /// </summary>
        IArticleJournalModel Journal { get; set; }
    }
}
