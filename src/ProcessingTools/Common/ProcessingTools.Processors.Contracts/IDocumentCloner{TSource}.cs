// <copyright file="IDocumentCloner{TSource}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    /// <summary>
    /// Generic document cloner.
    /// </summary>
    /// <typeparam name="TSource">Type of source of data to be cloned to the <see cref="IDocument"/> context</typeparam>
    public interface IDocumentCloner<in TSource> : ICloner<IDocument, TSource, object>
    {
    }
}
