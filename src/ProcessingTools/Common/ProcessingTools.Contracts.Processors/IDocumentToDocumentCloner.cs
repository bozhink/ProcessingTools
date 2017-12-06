// <copyright file="IDocumentToDocumentCloner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Processors
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// Cloner of <see cref="IDocument"/> object.
    /// </summary>
    public interface IDocumentToDocumentCloner : ICloner<IDocument, IDocument, object>
    {
    }
}
