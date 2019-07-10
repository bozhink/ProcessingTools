// <copyright file="IDocumentParser.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document parser.
    /// </summary>
    public interface IDocumentParser : IContextParser<IDocument, object>
    {
    }
}
