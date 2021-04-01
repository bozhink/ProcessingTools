// <copyright file="IDocumentProcessor.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document processor.
    /// </summary>
    public interface IDocumentProcessor : IContextProcessor<IDocument>
    {
    }
}
