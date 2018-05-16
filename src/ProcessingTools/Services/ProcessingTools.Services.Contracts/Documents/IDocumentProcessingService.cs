// <copyright file="IDocumentProcessingService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
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
    }
}
