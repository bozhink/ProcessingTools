﻿// <copyright file="IDocumentsWebService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using ProcessingTools.Web.Models.Documents.Documents;
using ProcessingTools.Web.Models.Shared;

namespace ProcessingTools.Contracts.Web.Services.Documents
{
    /// <summary>
    /// Documents web service.
    /// </summary>
    public interface IDocumentsWebService : IWebPresenter
    {
        /// <summary>
        /// Get <see cref="DocumentUploadViewModel"/>.
        /// </summary>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Task of <see cref="DocumentUploadViewModel"/>.</returns>
        Task<DocumentUploadViewModel> GetDocumentUploadViewModelAsync(string articleId);

        /// <summary>
        /// Get <see cref="DocumentEditViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Task of <see cref="DocumentEditViewModel"/>.</returns>
        Task<DocumentEditViewModel> GetDocumentEditViewModelAsync(string id, string articleId);

        /// <summary>
        /// Get <see cref="DocumentDeleteViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Task of <see cref="DocumentDeleteViewModel"/>.</returns>
        Task<DocumentDeleteViewModel> GetDocumentDeleteViewModelAsync(string id, string articleId);

        /// <summary>
        /// Get <see cref="DocumentDetailsViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Task of <see cref="DocumentDetailsViewModel"/>.</returns>
        Task<DocumentDetailsViewModel> GetDocumentDetailsViewModelAsync(string id, string articleId);

        /// <summary>
        /// Get <see cref="DocumentHtmlViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Task of <see cref="DocumentHtmlViewModel"/>.</returns>
        Task<DocumentHtmlViewModel> GetDocumentHtmlViewModelAsync(string id, string articleId);

        /// <summary>
        /// Get <see cref="DocumentXmlViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Task of <see cref="DocumentXmlViewModel"/>.</returns>
        Task<DocumentXmlViewModel> GetDocumentXmlViewModelAsync(string id, string articleId);

        /// <summary>
        /// Gets BadRequest response model.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns><see cref="MessageStatusResponseModel"/>.</returns>
        MessageStatusResponseModel GetBadRequestResponseModel(string id, string articleId);

        /// <summary>
        /// Gets InternalServerError response model.
        /// </summary>
        /// <param name="ex">Exception of the error.</param>
        /// <returns><see cref="MessageStatusResponseModel"/>.</returns>
        MessageStatusResponseModel GetInternalServerErrorResponseModel(Exception ex);

        /// <summary>
        /// Gets document saved response model.
        /// </summary>
        /// <param name="success">Success value.</param>
        /// <returns><see cref="MessageStatusResponseModel"/>.</returns>
        MessageStatusResponseModel GetDocumentSavedResponseModel(bool success);

        /// <summary>
        /// Gets <see cref="DocumentContentResponseModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="content">Content of the document.</param>
        /// <returns><see cref="DocumentContentResponseModel"/>.</returns>
        DocumentContentResponseModel GetDocumentContentResponseModel(string id, string articleId, string content);

        /// <summary>
        /// Upload document.
        /// </summary>
        /// <param name="formFile">Form file.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Success status.</returns>
        Task<bool> UploadDocumentAsync(Microsoft.AspNetCore.Http.IFormFile formFile, string articleId);

        /// <summary>
        /// Download document.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Task of <see cref="DocumentDownloadResponseModel"/>.</returns>
        Task<DocumentDownloadResponseModel> DownloadDocumentAsync(string id, string articleId);

        /// <summary>
        /// Update document.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> UpdateDocumentAsync(DocumentUpdateRequestModel model);

        /// <summary>
        /// Delete document.
        /// </summary>
        /// <param name="id">Object ID of the document to be deleted.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Success status.</returns>
        Task<bool> DeleteDocumentAsync(string id, string articleId);

        /// <summary>
        /// Sets specified document as final.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Success status.</returns>
        Task<bool> SetAsFinalAsync(string id, string articleId);

        /// <summary>
        /// Gets content of a specified document as HTML.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>HTML representation of the content of the document.</returns>
        Task<string> GetHtmlAsync(string id, string articleId);

        /// <summary>
        /// Gets content of a specified document as XML.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>XML representation of the content of the document.</returns>
        Task<string> GetXmlAsync(string id, string articleId);

        /// <summary>
        /// Sets HTML content of a specified document.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="content">HTML representation of the content of the document.</param>
        /// <returns>Success status.</returns>
        Task<bool> SetHtmlAsync(string id, string articleId, string content);

        /// <summary>
        /// Sets XML content of a specified document.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="content">XML representation of the content of the document.</param>
        /// <returns>Success status.</returns>
        Task<bool> SetXmlAsync(string id, string articleId, string content);

        /// <summary>
        /// Map <see cref="DocumentUpdateRequestModel"/> to <see cref="DocumentEditViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<DocumentEditViewModel> MapToViewModelAsync(DocumentUpdateRequestModel model);

        /// <summary>
        /// Map <see cref="DocumentDeleteRequestModel"/> to <see cref="DocumentDeleteViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<DocumentDeleteViewModel> MapToViewModelAsync(DocumentDeleteRequestModel model);
    }
}
