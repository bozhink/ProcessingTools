﻿// <copyright file="IDocumentProcessingService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Documents
{
    using System.Threading.Tasks;

    /// <summary>
    /// Document processing service.
    /// </summary>
    public interface IDocumentProcessingService
    {
        /// <summary>
        /// Parses references of a specified document.
        /// </summary>
        /// <param name="documentId">Object ID of the document to be processed.</param>
        /// <param name="articleId">Object ID of the article of the document.</param>
        /// <returns>Resultant object.</returns>
        Task<object> ParseReferencesAsync(object documentId, object articleId);

        /// <summary>
        /// Tags references of a specified document.
        /// </summary>
        /// <param name="documentId">Object ID of the document to be processed.</param>
        /// <param name="articleId">Object ID of the article of the document.</param>
        /// <returns>Resultant object.</returns>
        Task<object> TagReferencesAsync(object documentId, object articleId);

        /// <summary>
        /// Updates document meta-data.
        /// </summary>
        /// <param name="documentId">Object ID of the document to be processed.</param>
        /// <param name="articleId">Object ID of the article of the document.</param>
        /// <returns>Resultant object.</returns>
        Task<object> UpdateDocumentMetaAsync(object documentId, object articleId);

        /// <summary>
        /// Updates article documents meta-data.
        /// </summary>
        /// <param name="articleId">Object ID of the article of the document.</param>
        /// <returns>Resultant object.</returns>
        Task<object> UpdateArticleDocumentsMetaAsync(object articleId);
    }
}
