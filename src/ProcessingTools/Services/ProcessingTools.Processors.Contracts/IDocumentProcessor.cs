// <copyright file="IDocumentProcessor.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// Document processor.
    /// </summary>
    public interface IDocumentProcessor : IContextProcessor<IDocument>
    {
    }
}
