// <copyright file="IArticleDetailsDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Documents.Articles
{
    using ProcessingTools.Contracts.Models.Documents.Articles;

    /// <summary>
    /// Article details data transfer object (DTO).
    /// </summary>
    public interface IArticleDetailsDataTransferObject : IArticleDataTransferObject, IArticleDetailsModel
    {
        /// <summary>
        /// Gets the article journal.
        /// </summary>
        IArticleJournalDataTransferObject Journal { get; }
    }
}
