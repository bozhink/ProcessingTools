// <copyright file="IDocumentParser.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// Document parser.
    /// </summary>
    public interface IDocumentParser : IContextParser<IDocument, object>
    {
    }
}
