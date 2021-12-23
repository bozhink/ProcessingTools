// <copyright file="IDocumentFormatter.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document formatter.
    /// </summary>
    public interface IDocumentFormatter : IContextFormatter<IDocument, object>
    {
    }
}
