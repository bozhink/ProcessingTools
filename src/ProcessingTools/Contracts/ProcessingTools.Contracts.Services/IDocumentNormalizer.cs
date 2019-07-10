// <copyright file="IDocumentNormalizer.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Document normalizer.
    /// </summary>
    public interface IDocumentNormalizer
    {
        /// <summary>
        /// Normalizes document content.
        /// </summary>
        /// <param name="document"><see cref="IDocument"/> object to be normalized.</param>
        /// <returns>Task.</returns>
        Task<object> NormalizeAsync(IDocument document);
    }
}
