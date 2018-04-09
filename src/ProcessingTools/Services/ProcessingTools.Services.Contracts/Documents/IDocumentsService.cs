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
        /// Uploads new document from file.
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
        /// <returns>Resultant object.</returns>
        Task<object> UpdateAsync(IDocumentUpdateModel model);

        /// <summary>
        /// Delete document.
        /// </summary>
        /// <param name="id">Object ID of the document to be deleted.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Resultant object.</returns>
        Task<object> DeleteAsync(string id, string articleId);

        /// <summary>
        /// Sets specified document as final.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Resultant object.</returns>
        Task<object> SetAsFinalAsync(string id, string articleId);

        /// <summary>
        /// Gets document by ID.
        /// </summary>
        /// <param name="id">Object ID of the document to be deleted.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Document.</returns>
        Task<IDocumentModel> GetByIdAsync(string id, string articleId);

        /// <summary>
        /// Gets document details by ID.
        /// </summary>
        /// <param name="id">Object ID of the document to be deleted.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Document details.</returns>
        Task<IDocumentDetailsModel> GetDetailsByIdAsync(string id, string articleId);

        /// <summary>
        /// Gets the article of documents.
        /// </summary>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Document article.</returns>
        Task<IDocumentArticle> GetDocumentArticleAsync(string articleId);

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
        /// <returns>Resultant object.</returns>
        Task<object> SetHtmlAsync(string id, string articleId, string content);

        /// <summary>
        /// Sets XML content of a specified document.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="content">XML representation of the content of the document.</param>
        /// <returns>Resultant object.</returns>
        Task<object> SetXmlAsync(string id, string articleId, string content);
    }
}
