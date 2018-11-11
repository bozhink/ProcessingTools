// <copyright file="IDocumentMetaResolver.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Documents.Meta;

    /// <summary>
    /// Resolver of document meta-data.
    /// </summary>
    public interface IDocumentMetaResolver
    {
        /// <summary>
        /// Gets full document meta-information by specified document ID.
        /// </summary>
        /// <param name="documentId">ID of the document to be retrieved.</param>
        /// <returns>Task of document model.</returns>
        Task<IDocumentFullModel> GetDocumentAsync(string documentId);

        /// <summary>
        /// Gets full article meta-information by specified article ID.
        /// </summary>
        /// <param name="articleId">ID of the article to be retrieved.</param>
        /// <returns>Task of article model.</returns>
        Task<IArticleFullModel> GetArticleAsync(string articleId);

        /// <summary>
        /// Gets full article documents meta-information by specified article ID.
        /// </summary>
        /// <param name="articleId">ID of the article to be retrieved.</param>
        /// <returns>Task of article documents model.</returns>
        Task<IArticleDocumentsFullModel> GetArticleDocumentsAsync(string articleId);
    }
}
