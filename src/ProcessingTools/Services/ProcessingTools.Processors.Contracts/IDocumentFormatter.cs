// <copyright file="IDocumentFormatter.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// Document formatter.
    /// </summary>
    public interface IDocumentFormatter : IContextFormatter<IDocument, object>
    {
    }
}
