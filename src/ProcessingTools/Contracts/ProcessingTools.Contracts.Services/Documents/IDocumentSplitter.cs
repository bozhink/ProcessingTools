// <copyright file="IDocumentSplitter.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Collections.Generic;
using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services.Documents
{
    /// <summary>
    /// Document splitter.
    /// </summary>
    public interface IDocumentSplitter
    {
        /// <summary>
        /// Splits document into sub-document chunks.
        /// </summary>
        /// <param name="document">Document to be split.</param>
        /// <returns>Sub-document chunks.</returns>
        IEnumerable<IDocument> Split(IDocument document);
    }
}
