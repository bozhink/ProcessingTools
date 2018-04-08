// <copyright file="IDocumentsService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Documents service.
    /// </summary>
    public interface IDocumentsService
    {
        /// <summary>
        /// Uploads new document from file..
        /// </summary>
        /// <param name="model">Document file model.</param>
        /// <param name="stream">Stream of the document's content.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Resultant object.</returns>
        Task<object> UploadAsync(IDocumentFileModel model, Stream stream, string articleId);

        /// <summary>
        /// Download document.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Task of <see cref="IDocumentFileStreamModel"/>.</returns>
        Task<IDocumentFileStreamModel> DownloadAsync(string id, string articleId);

        /// <summary>
        /// Update document.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> UpdateDocumentAsync(IDocumentUpdateModel model);

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
    }
}
