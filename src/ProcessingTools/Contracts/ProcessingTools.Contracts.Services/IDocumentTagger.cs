// <copyright file="IDocumentTagger.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document tagger.
    /// </summary>
    public interface IDocumentTagger : IContextTagger<IDocument, object>
    {
    }
}
