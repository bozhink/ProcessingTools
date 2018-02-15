// <copyright file="IDocumentXslProcessor.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    /// <summary>
    /// Document XSL processor.
    /// </summary>
    public interface IDocumentXslProcessor : IDocumentProcessor
    {
        /// <summary>
        /// Gets or sets XSL file name.
        /// </summary>
        string XslFileName { get; set; }
    }
}
