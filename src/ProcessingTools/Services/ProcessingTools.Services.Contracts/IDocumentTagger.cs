// <copyright file="IDocumentTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Document tagger.
    /// </summary>
    public interface IDocumentTagger : IContextTagger<IDocument, object>
    {
    }
}
