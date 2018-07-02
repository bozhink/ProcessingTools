// <copyright file="IDocumentsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Data.Models.Contracts.Documents.Documents;
    using ProcessingTools.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Documents data access object.
    /// </summary>
    public interface IDocumentsDataAccessObject : IDataAccessObject<IDocumentDataModel, IDocumentDetailsDataModel, IDocumentInsertModel, IDocumentUpdateModel>
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
        Task<IDocumentDataModel[]> GetArticleDocumentsAsync(string articleId);

        /// <summary>
        /// Gets document's article.
        /// </summary>
        /// <param name="articleId">ID of the article.</param>
        /// <returns>Document's article.</returns>
        Task<IDocumentArticleDataModel> GetDocumentArticleAsync(string articleId);

        /// <summary>
        /// Sets specified document as final.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Resultant object.</returns>
        Task<object> SetAsFinalAsync(object id, string articleId);
    }
}
