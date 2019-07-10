// <copyright file="IDocumentParser.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Document parser.
    /// </summary>
    public interface IDocumentParser : IContextParser<IDocument, object>
    {
    }
}
