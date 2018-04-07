// <copyright file="IDocumentsService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Documents.Documents;

    /// <summary>
    /// Documents service.
    /// </summary>
    public interface IDocumentsService
    {
        /// <summary>
        /// Get <see cref="DocumentUploadViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="DocumentUploadViewModel"/>.</returns>
        Task<DocumentUploadViewModel> GetDocumentUploadViewModelAsync();

        /// <summary>
        /// Get <see cref="DocumentEditViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <returns>Task of <see cref="DocumentEditViewModel"/>.</returns>
        Task<DocumentEditViewModel> GetDocumentEditViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="DocumentDeleteViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <returns>Task of <see cref="DocumentDeleteViewModel"/>.</returns>
        Task<DocumentDeleteViewModel> GetDocumentDeleteViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="DocumentDetailsViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <returns>Task of <see cref="DocumentDetailsViewModel"/>.</returns>
        Task<DocumentDetailsViewModel> GetDocumentDetailsViewModelAsync(string id);

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
        /// <returns>Task of <see cref="DocumentDownloadResponseModel"/>.</returns>
        Task<DocumentDownloadResponseModel> DownloadDocumentAsync(string id);

        /// <summary>
        /// Update document.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> UpdateDocumentAsync(DocumentUpdateRequestModel model);

        /// <summary>
        /// Delete document.
        /// </summary>
        /// <param name="id">ID of the article to be deleted.</param>
        /// <returns>Success status.</returns>
        Task<bool> DeleteDocumentAsync(string id);

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
