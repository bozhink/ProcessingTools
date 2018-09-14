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
        /// Gets full document meta information by specified document ID.
        /// </summary>
        /// <param name="documentId">ID of the document to be retrieved.</param>
        /// <returns>Task of document model.</returns>
        Task<IDocumentFullModel> GetDocumentAsync(object documentId);
    }
}
