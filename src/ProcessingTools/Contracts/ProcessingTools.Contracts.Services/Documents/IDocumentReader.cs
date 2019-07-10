// <copyright file="IDocumentReader.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document reader.
    /// </summary>
    public interface IDocumentReader
    {
        /// <summary>
        /// Read document by specified file name.
        /// </summary>
        /// <param name="fileName">File name of the document to be read.</param>
        /// <returns>Read document.</returns>
        Task<IDocument> ReadDocumentAsync(string fileName);
    }
}
