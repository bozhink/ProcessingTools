// <copyright file="IDocumentFormatter.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Document formatter.
    /// </summary>
    public interface IDocumentFormatter : IContextFormatter<IDocument, object>
    {
    }
}
