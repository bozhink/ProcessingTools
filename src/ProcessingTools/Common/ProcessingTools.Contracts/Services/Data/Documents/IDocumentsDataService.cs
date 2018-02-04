// <copyright file="IDocumentsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Documents
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Models.Contracts.Services.Data.Documents;

    /// <summary>
    /// Documents data service.
    /// </summary>
    public interface IDocumentsDataService
    {
        /// <summary>
        /// Gets all documents.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID of the parent article.</param>
        /// <param name="pageNumber">Page number of items to take.</param>
        /// <param name="itemsPerPage">Number of items per page.</param>
        /// <returns>Array of documents.</returns>
        Task<IDocument[]> AllAsync(object userId, object articleId, int pageNumber, int itemsPerPage);

        /// <summary>
        /// Gets count of documents.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID of the parent article.</param>
        /// <returns>Number of documents.</returns>
        Task<long> CountAsync(object userId, object articleId);

        /// <summary>
        /// Creates new document.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID of the parent article.</param>
        /// <param name="document"><see cref="IDocument"/> model.</param>
        /// <param name="inputStream">Input stream for the document content.</param>
        /// <returns>Task of result.</returns>
        Task<object> CreateAsync(object userId, object articleId, IDocument document, Stream inputStream);

        /// <summary>
        /// Deletes a document.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID of the parent article.</param>
        /// <param name="documentId">Document ID of the document to be deleted.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteAsync(object userId, object articleId, object documentId);

        /// <summary>
        /// Deletes all documents per specified article.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID of the parent article.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteAllAsync(object userId, object articleId);

        /// <summary>
        /// Gets a single document.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID of the parent article.</param>
        /// <param name="documentId">Document ID of the document.</param>
        /// <returns><see cref="IDocument"/> instance or null.</returns>
        Task<IDocument> GetAsync(object userId, object articleId, object documentId);

        /// <summary>
        /// Gets <see cref="XmlReader"/> for document's content.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID of the parent article.</param>
        /// <param name="documentId">Document ID of the document.</param>
        /// <returns><see cref="XmlReader"/> for document's content.</returns>
        Task<XmlReader> GetReaderAsync(object userId, object articleId, object documentId);

        /// <summary>
        /// Gets <see cref="Stream"/> of the document's content.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID of the parent article.</param>
        /// <param name="documentId">Document ID of the document.</param>
        /// <returns><see cref="Stream"/> of the document's content.</returns>
        Task<Stream> GetStreamAsync(object userId, object articleId, object documentId);

        /// <summary>
        /// Updates only the metadata of a specified <see cref="IDocument"/> object.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID of the parent article.</param>
        /// <param name="document"><see cref="IDocument"/> to be updated.</param>
        /// <returns>Task of result.</returns>
        Task<object> UpdateMetaAsync(object userId, object articleId, IDocument document);

        /// <summary>
        /// Updates content of a specified <see cref="IDocument"/> object.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="articleId">Article ID of the parent article.</param>
        /// <param name="document"><see cref="IDocument"/> to be updated.</param>
        /// <param name="content">String content of the document to be set.</param>
        /// <returns>Task of result.</returns>
        Task<object> UpdateContentAsync(object userId, object articleId, IDocument document, string content);
    }
}
