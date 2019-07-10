// <copyright file="IDocumentProcessor.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document processor.
    /// </summary>
    public interface IDocumentProcessor : IContextProcessor<IDocument>
    {
    }
}
