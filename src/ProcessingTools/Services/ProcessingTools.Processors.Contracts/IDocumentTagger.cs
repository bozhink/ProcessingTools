// <copyright file="IDocumentTagger.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// Document tagger.
    /// </summary>
    public interface IDocumentTagger : IContextTagger<IDocument, object>
    {
    }
}
