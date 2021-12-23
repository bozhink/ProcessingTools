// <copyright file="IDocumentToDocumentCloner.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Cloner of <see cref="IDocument"/> object.
    /// </summary>
    public interface IDocumentToDocumentCloner : ICloner<IDocument, IDocument, object>
    {
    }
}
