// <copyright file="IDocumentsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Services.Models.Documents.Documents;

namespace ProcessingTools.Contracts.Services.Documents
{
    /// <summary>
    /// Documents data service.
    /// </summary>
    public interface IDocumentsDataService : IDataService<IDocumentModel, IDocumentDetailsModel, IDocumentInsertModel, IDocumentUpdateModel>
    {
        /// <summary>
        /// Sets content of the specified document.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="content">Content as string of the document.</param>
        /// <returns>Length of the written content.</returns>
        Task<long> SetDocumentContentAsync(object id, string content);

        /// <summary>
        /// Gets content of the specified documents.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <returns>Content of the document as string.</returns>
        Task<string> GetDocumentContentAsync(object id);

        /// <summary>
        /// Gets documents of a specified article.
        /// </summary>
        /// <param name="articleId">ID of the article.</param>
        /// <returns>Array of documents.</returns>
        Task<IDocumentModel[]> GetArticleDocumentsAsync(string articleId);

        /// <summary>
        /// Gets the article of documents.
        /// </summary>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Document article.</returns>
        Task<IDocumentArticleModel> GetDocumentArticleAsync(string articleId);

        /// <summary>
        /// Sets specified document as final.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Resultant object.</returns>
        Task<object> SetAsFinalAsync(object id, string articleId);
    }
}
