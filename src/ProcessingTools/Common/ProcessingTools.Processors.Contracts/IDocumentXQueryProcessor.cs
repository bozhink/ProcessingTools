// <copyright file="IDocumentXQueryProcessor.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    /// <summary>
    /// Document XQuery processor.
    /// </summary>
    public interface IDocumentXQueryProcessor : IDocumentProcessor
    {
        /// <summary>
        /// Gets or sets XQuery file full name.
        /// </summary>
        string XQueryFileName { get; set; }
    }
}
