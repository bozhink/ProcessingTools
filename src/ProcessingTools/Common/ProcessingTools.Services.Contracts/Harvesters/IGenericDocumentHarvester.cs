// <copyright file="IGenericDocumentHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters
{
    public interface IGenericDocumentHarvester<T> : IHarvester<IDocument, T>
    {
    }
}
