﻿// <copyright file="IKeyValuePairsRepository.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a [key]-[value] pairs data repository.
    /// </summary>
    /// <typeparam name="TKey">Type of the key-objects.</typeparam>
    /// <typeparam name="TValue">Type of value-objects.</typeparam>
    public interface IKeyValuePairsRepository<TKey, TValue> : IRepository<TValue>, IKeyListableRepository<TKey>
    {
        /// <summary>
        /// Adds a new key-value pair to the data repository.
        /// </summary>
        /// <param name="key">The key-object to be created.</param>
        /// <param name="value">The value-object to be set as value to the key.</param>
        /// <returns>Task</returns>
        Task<object> AddAsync(TKey key, TValue value);

        /// <summary>
        /// Gets the value under the key.
        /// </summary>
        /// <param name="key">The key-object to be requested.</param>
        /// <returns>The value under the requested key-object.</returns>
        Task<TValue> GetAsync(TKey key);

        /// <summary>
        /// Removes the key-object with its underlying value-object.
        /// </summary>
        /// <param name="key">The key-object to be requested.</param>
        /// <returns>Task</returns>
        Task<object> RemoveAsync(TKey key);

        /// <summary>
        /// Updates the value of an existing key-object.
        /// </summary>
        /// <param name="key">The key-object which value will be updated.</param>
        /// <param name="value">The new value to be set to the key-object.</param>
        /// <returns>Task</returns>
        Task<object> UpdateAsync(TKey key, TValue value);

        /// <summary>
        /// Creates new or updates existing key-value pair.
        /// </summary>
        /// <param name="key">The key-object which value will be created or updated.</param>
        /// <param name="value">The value to be set to the key-object.</param>
        /// <returns>Task</returns>
        Task<object> UpsertAsync(TKey key, TValue value);
    }
}
