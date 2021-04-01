// <copyright file="IDocumentXQueryProcessor.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Document XQuery processor.
    /// </summary>
    public interface IDocumentXQueryProcessor : IDocumentProcessor
    {
        /// <summary>
        /// Gets or sets XQuery file name.
        /// </summary>
        string XQueryFileName { get; set; }
    }
}
