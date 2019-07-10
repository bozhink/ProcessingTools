﻿// <copyright file="IDocumentWriter.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services.Documents
{
    /// <summary>
    /// Document writer.
    /// </summary>
    public interface IDocumentWriter
    {
        /// <summary>
        /// Writes document to specified file name.
        /// </summary>
        /// <param name="fileName">File name of the output document.</param>
        /// <param name="document">Document to be written.</param>
        /// <returns>Task.</returns>
        Task<object> WriteDocumentAsync(string fileName, IDocument document);
    }
}
