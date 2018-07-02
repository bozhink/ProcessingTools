// <copyright file="IDocumentHarvester{TModel}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Contracts
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// Generic harvester with <see cref="IDocument"/> context.
    /// </summary>
    /// <typeparam name="TModel">Type of the harvester model.</typeparam>
    public interface IDocumentHarvester<TModel> : IHarvester<IDocument, TModel>
    {
    }
}
