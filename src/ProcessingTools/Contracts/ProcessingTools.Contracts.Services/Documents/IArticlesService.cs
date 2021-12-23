﻿// <copyright file="IArticlesService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Documents
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Models.Documents.Articles;
    using ProcessingTools.Contracts.Services.Models.Documents.Documents;

    /// <summary>
    /// Articles service.
    /// </summary>
    public interface IArticlesService
    {
        /// <summary>
        /// Creates new article.
        /// </summary>
        /// <param name="model">Article model to be inserted.</param>
        /// <returns>Resultant object.</returns>
        Task<object> CreateAsync(IArticleInsertModel model);

        /// <summary>
        /// Creates new article from file.
        /// </summary>
        /// <param name="model">Article file model.</param>
        /// <param name="stream">Stream of the article's content.</param>
        /// <param name="journalId">Journal ID.</param>
        /// <returns>Resultant object.</returns>
        Task<object> CreateFromFileAsync(IArticleFileModel model, Stream stream, string journalId);

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
        /// Gets details of article by ID.
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
        Task<IList<IArticleDetailsModel>> SelectDetailsAsync(int skip, int take);

        /// <summary>
        /// Get number of articles.
        /// </summary>
        /// <returns>Number of articles.</returns>
        Task<long> SelectCountAsync();

        /// <summary>
        /// Gets journal publishers for select.
        /// </summary>
        /// <returns>Array of journal publishers.</returns>
        Task<IList<IArticleJournalModel>> GetArticleJournalsAsync();

        /// <summary>
        /// Gets documents of a specified article.
        /// </summary>
        /// <param name="articleId">ID of the article.</param>
        /// <returns>Array of documents.</returns>
        Task<IList<IDocumentModel>> GetArticleDocumentsAsync(string articleId);

        /// <summary>
        /// Finalizes article specified by object ID.
        /// </summary>
        /// <param name="id">Object ID of the article.</param>
        /// <returns>Resultant object.</returns>
        Task<object> FinalizeAsync(object id);
    }
}
