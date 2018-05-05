// <copyright file="IDocumentSchemaNormalizer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Layout
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Provides normalization for IDocument objects.
    /// </summary>
    public interface IDocumentSchemaNormalizer
    {
        /// <summary>
        /// Normalizes the IDocument object's xml to its current SchemaType.
        /// </summary>
        /// <param name="document">IDocument object to be normalized.</param>
        /// <returns>Task</returns>
        Task<object> NormalizeToDocumentSchemaAsync(IDocument document);

        /// <summary>
        /// Normalizes the IDocument object's xml content to the system schema.
        /// </summary>
        /// <param name="document">IDocument object to be normalized.</param>
        /// <returns>Task</returns>
        Task<object> NormalizeToSystemAsync(IDocument document);
    }
}
