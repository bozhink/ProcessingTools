// <copyright file="IMemoryKeyValueDataStore{TKey,TValue}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Memory.Contracts
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Memory key-value data store.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    public interface IMemoryKeyValueDataStore<TKey, TValue>
    {
        /// <summary>
        /// Gets all keys in the data store.
        /// </summary>
        IEnumerable<TKey> Keys { get; }

        /// <summary>
        /// Gets value under the specified key.
        /// </summary>
        /// <param name="key">Value of the key object.</param>
        /// <returns>Value under the specified key.</returns>
        TValue this[TKey key] { get; }

        /// <summary>
        /// Adds or updates key-value pair.
        /// </summary>
        /// <param name="key">Value of the key object.</param>
        /// <param name="addValueFactory">Add value callback.</param>
        /// <param name="updateValueFactory">Update value callback.</param>
        /// <returns>Resultant value under the specified key.</returns>
        TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory);

        /// <summary>
        /// Removes key and it under-lying value.
        /// </summary>
        /// <param name="key">Value of the key object.</param>
        /// <returns>Success status.</returns>
        bool Remove(TKey key);
    }
}
