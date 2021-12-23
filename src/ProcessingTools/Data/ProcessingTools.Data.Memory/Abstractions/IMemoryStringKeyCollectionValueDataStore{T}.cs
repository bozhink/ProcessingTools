// <copyright file="IMemoryStringKeyCollectionValueDataStore{T}.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Memory.Abstractions
{
    using System.Collections.Generic;

    /// <summary>
    /// Memory string-key-collection-value data store.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public interface IMemoryStringKeyCollectionValueDataStore<T> : IMemoryKeyValueDataStore<string, ICollection<T>>
    {
    }
}
