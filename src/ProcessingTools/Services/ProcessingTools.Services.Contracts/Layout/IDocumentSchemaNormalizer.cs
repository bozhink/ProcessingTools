// <copyright file="IDocumentSchemaNormalizer.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services.Layout
{
    /// <summary>
    /// Provides normalization for IDocument objects.
    /// </summary>
    public interface IDocumentSchemaNormalizer
    {
        /// <summary>
        /// Normalizes the IDocument object's XML to its current SchemaType.
        /// </summary>
        /// <param name="document">IDocument object to be normalized.</param>
        /// <returns>Task.</returns>
        Task<object> NormalizeToDocumentSchemaAsync(IDocument document);

        /// <summary>
        /// Normalizes the IDocument object's XML content to the system schema.
        /// </summary>
        /// <param name="document">IDocument object to be normalized.</param>
        /// <returns>Task.</returns>
        Task<object> NormalizeToSystemAsync(IDocument document);
    }
}
