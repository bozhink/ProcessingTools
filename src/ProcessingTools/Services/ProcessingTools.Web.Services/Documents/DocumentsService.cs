// <copyright file="DocumentsService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Documents
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using ProcessingTools.Web.Models.Documents.Documents;

    /// <summary>
    /// Documents service.
    /// </summary>
    public class DocumentsService : ProcessingTools.Web.Services.Contracts.Documents.IDocumentsService
    {
        /// <inheritdoc/>
        public async Task<bool> DeleteDocumentAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return false;
            }

            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<DocumentDownloadResponseModel> DownloadDocumentAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return new DocumentDownloadResponseModel();
            }

            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<DocumentDeleteViewModel> GetDocumentDeleteViewModelAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<DocumentDetailsViewModel> GetDocumentDetailsViewModelAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<DocumentEditViewModel> GetDocumentEditViewModelAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<DocumentUploadViewModel> GetDocumentUploadViewModelAsync(string articleId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<string> GetHtmlAsync(string id, string articleId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<string> GetXmlAsync(string id, string articleId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<DocumentEditViewModel> MapToViewModelAsync(DocumentUpdateRequestModel model)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<DocumentDeleteViewModel> MapToViewModelAsync(DocumentDeleteRequestModel model)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<bool> SetAsFinalAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return false;
            }

            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<bool> SetHtmlAsync(string id, string articleId, string content)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return false;
            }

            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<bool> SetXmlAsync(string id, string articleId, string content)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return false;
            }

            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateDocumentAsync(DocumentUpdateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<bool> UploadDocumentAsync(IFormFile formFile, string articleId)
        {
            if (formFile == null || string.IsNullOrWhiteSpace(articleId))
            {
                return false;
            }

            throw new System.NotImplementedException();
        }
    }
}
