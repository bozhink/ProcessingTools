// <copyright file="IDocumentProcessor.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Document processor.
    /// </summary>
    public interface IDocumentProcessor : IContextProcessor<IDocument>
    {
    }
}
