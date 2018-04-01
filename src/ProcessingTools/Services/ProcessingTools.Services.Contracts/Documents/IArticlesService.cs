// <copyright file="IArticlesService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;

    /// <summary>
    /// Articles service.
    /// </summary>
    public interface IArticlesService
    {
        /// <summary>
        /// Inserts new article.
        /// </summary>
        /// <param name="model">Article model to be inserted.</param>
        /// <returns>Resultant object.</returns>
        Task<object> InsertAsync(IArticleInsertModel model);

        /// <summary>
        /// Deletes article specified by ID.
        /// </summary>
        /// <param name="id">ID of the article to be deleted.</param>
        /// <returns>Resultant object.</returns>
        Task<object> DeleteAsync(object id);

        /// <summary>
        /// Updates article.
        /// </summary>
        /// <param name="model">Article model to update.</param>
        /// <returns>Resultant object.</returns>
        Task<object> UpdateAsync(IArticleUpdateModel model);

        /// <summary>
        /// Gets details of article by ID;
        /// </summary>
        /// <param name="id">ID of the article.</param>
        /// <returns>Article details.</returns>
        Task<IArticleDetailsModel> GetDetailsByIdAsync(object id);

        /// <summary>
        /// Select details of articles for pagination.
        /// </summary>
        /// <param name="skip">Number of article items to skip.</param>
        /// <param name="take">Number of article items to take.</param>
        /// <returns>Array of article details.</returns>
        Task<IArticleDetailsModel[]> SelectDetailsAsync(int skip, int take);

        /// <summary>
        /// Get number of articles.
        /// </summary>
        /// <returns>Number of articles.</returns>
        Task<long> SelectCountAsync();

        /// <summary>
        /// Gets journal publishers for select.
        /// </summary>
        /// <returns>Array of journal publishers.</returns>
        Task<IArticleJournalModel[]> GetArticleJournalsAsync();
    }
}
