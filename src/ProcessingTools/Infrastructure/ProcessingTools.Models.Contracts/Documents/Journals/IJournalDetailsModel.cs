// <copyright file="IJournalDetailsModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Journals
{
    /// <summary>
    /// Journal details model.
    /// </summary>
    public interface IJournalDetailsModel : IJournalModel
    {
        /// <summary>
        /// Gets the number of articles.
        /// </summary>
        long NumberOfArticles { get; }
    }
}
