// <copyright file="IDocumentMetaService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using System.Threading.Tasks;

    /// <summary>
    /// Service for processing document meta-data.
    /// </summary>
    public interface IDocumentMetaService
    {
        /// <summary>
        /// Updates the meta-data of a document, specified by its ID.
        /// </summary>
        /// <param name="documentId">The ID of the document to be processed.</param>
        /// <returns>Task of result.</returns>
        Task<object> UpdateDocumentAsync(string documentId);

        /// <summary>
        /// Updates the meta-data of all documents of an article, specified by its ID.
        /// </summary>
        /// <param name="articleId">The ID of the article to be processed.</param>
        /// <returns>Task of result.</returns>
        Task<object> UpdateArticleDocumentsAsync(string articleId);
    }
}
