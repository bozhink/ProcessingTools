// <copyright file="IDocumentReader.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

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
