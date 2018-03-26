// <copyright file="IArticlesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;

    /// <summary>
    /// Articles data service.
    /// </summary>
    public interface IArticlesDataService : IDataService<IArticleModel, IArticleDetailsModel, IArticleInsertModel, IArticleUpdateModel>
    {
        /// <summary>
        /// Gets journal publishers for select.
        /// </summary>
        /// <returns>Array of journal publishers.</returns>
        Task<IArticleJournalModel[]> GetArticleJournalsAsync();
    }
}
