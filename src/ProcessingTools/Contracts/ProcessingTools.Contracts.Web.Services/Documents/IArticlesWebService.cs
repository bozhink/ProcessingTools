﻿// <copyright file="IArticlesWebService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Web.Models.Documents.Articles;

namespace ProcessingTools.Contracts.Web.Services.Documents
{
    /// <summary>
    /// Articles web service.
    /// </summary>
    public interface IArticlesWebService : IWebPresenter
    {
        /// <summary>
        /// Get <see cref="ArticlesIndexViewModel"/>.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Task of <see cref="ArticlesIndexViewModel"/>.</returns>
        Task<ArticlesIndexViewModel> GetArticlesIndexViewModelAsync(int skip, int take);

        /// <summary>
        /// Get <see cref="ArticleCreateViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="ArticleCreateViewModel"/>.</returns>
        Task<ArticleCreateViewModel> GetArticleCreateViewModelAsync();

        /// <summary>
        /// Get <see cref="ArticleCreateFromFileViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="ArticleCreateFromFileViewModel"/>.</returns>
        Task<ArticleCreateFromFileViewModel> GetArticleCreateFromFileViewModelAsync();

        /// <summary>
        /// Get <see cref="ArticleEditViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the article.</param>
        /// <returns>Task of <see cref="ArticleEditViewModel"/>.</returns>
        Task<ArticleEditViewModel> GetArticleEditViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="ArticleDeleteViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the article.</param>
        /// <returns>Task of <see cref="ArticleDeleteViewModel"/>.</returns>
        Task<ArticleDeleteViewModel> GetArticleDeleteViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="ArticleDetailsViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the article.</param>
        /// <returns>Task of <see cref="ArticleDetailsViewModel"/>.</returns>
        Task<ArticleDetailsViewModel> GetArticleDetailsViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="ArticleDocumentsViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the article.</param>
        /// <returns>Task of <see cref="ArticleDocumentsViewModel"/>.</returns>
        Task<ArticleDocumentsViewModel> GetArticleDocumentsViewModelAsync(string id);

        /// <summary>
        /// Create article.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> CreateArticleAsync(ArticleCreateRequestModel model);

        /// <summary>
        /// Create article.
        /// </summary>
        /// <param name="formFile">Form file.</param>
        /// <param name="journalId">Journal ID.</param>
        /// <returns>Success status.</returns>
        Task<bool> CreateFromFileArticleAsync(Microsoft.AspNetCore.Http.IFormFile formFile, string journalId);

        /// <summary>
        /// Update article.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> UpdateArticleAsync(ArticleUpdateRequestModel model);

        /// <summary>
        /// Delete article.
        /// </summary>
        /// <param name="id">ID of the article to be deleted.</param>
        /// <returns>Success status.</returns>
        Task<bool> DeleteArticleAsync(string id);

        /// <summary>
        /// Finalizes article specified by object ID.
        /// </summary>
        /// <param name="id">Object ID of the article.</param>
        /// <returns>Success status.</returns>
        Task<bool> FinalizeArticleAsync(string id);

        /// <summary>
        /// Map <see cref="ArticleCreateRequestModel"/> to <see cref="ArticleCreateViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<ArticleCreateViewModel> MapToViewModelAsync(ArticleCreateRequestModel model);

        /// <summary>
        /// Map <see cref="ArticleUpdateRequestModel"/> to <see cref="ArticleEditViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<ArticleEditViewModel> MapToViewModelAsync(ArticleUpdateRequestModel model);

        /// <summary>
        /// Map <see cref="ArticleDeleteRequestModel"/> to <see cref="ArticleDeleteViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<ArticleDeleteViewModel> MapToViewModelAsync(ArticleDeleteRequestModel model);
    }
}
