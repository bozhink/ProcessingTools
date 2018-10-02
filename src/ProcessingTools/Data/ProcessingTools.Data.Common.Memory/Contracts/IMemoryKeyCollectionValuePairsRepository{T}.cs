// <copyright file="IMemoryKeyCollectionValuePairsRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Memory.Contracts
{
    using ProcessingTools.Data.Contracts;

    /// <summary>
    /// Memory key-collection-value pairs repository.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public interface IMemoryKeyCollectionValuePairsRepository<T> : IStringKeyCollectionValuePairsRepository<T>
    {
    }
}
