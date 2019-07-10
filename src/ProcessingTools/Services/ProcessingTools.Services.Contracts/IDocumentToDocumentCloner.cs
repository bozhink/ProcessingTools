// <copyright file="IDocumentToDocumentCloner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Cloner of <see cref="IDocument"/> object.
    /// </summary>
    public interface IDocumentToDocumentCloner : ICloner<IDocument, IDocument, object>
    {
    }
}
