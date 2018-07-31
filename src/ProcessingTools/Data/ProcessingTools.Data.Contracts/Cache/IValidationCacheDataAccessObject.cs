﻿// <copyright file="IValidationCacheDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Cache
{
    using System.Threading.Tasks;
    using ProcessingTools.Data.Models.Contracts.Cache;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Cache;

    /// <summary>
    /// Validation cache data access object.
    /// </summary>
    public interface IValidationCacheDataAccessObject
    {
        /// <summary>
        /// Adds new item per specified key.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <param name="model">Model item to be added to the key.</param>
        /// <returns>Task of result.</returns>
        Task<object> AddAsync(string key, IValidationCacheModel model);

        /// <summary>
        /// Removes item from specified key.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <param name="model">>Model item to be removed from the key.</param>
        /// <returns>Task of result.</returns>
        Task<object> RemoveAsync(string key, IValidationCacheModel model);

        /// <summary>
        /// Removes entire key with all data under it.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns>Task of result.</returns>
        Task<object> RemoveAsync(string key);

        /// <summary>
        /// Clear the cache store. Removes all keys and all the data under them.
        /// </summary>
        /// <returns>Task of result.</returns>
        Task<object> ClearCacheAsync();

        /// <summary>
        /// Gets the last cache item registered for specified key.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns>The last cache item registered for the specified key.</returns>
        Task<IValidationCacheDataModel> GetLastForKeyAsync(string key);

        /// <summary>
        /// Gets all the cache items registered for specified key.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns>All the cache items registered for specified key.</returns>
        Task<IValidationCacheDataModel[]> GetAllForKeyAsync(string key);

        /// <summary>
        /// Select all items with specified criteria.
        /// </summary>
        /// <param name="filter">Filter value for content.</param>
        /// <param name="sortOrder">Sort order.</param>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>All the cache items matching specified criteria.</returns>
        Task<IValidationCacheDataModel[]> SelectAsync(string filter, SortOrder sortOrder, int skip, int take);

        /// <summary>
        /// Gets the count of all items in the cache.
        /// </summary>
        /// <returns>The count of all items in the cache</returns>
        Task<long> CountAsync();
    }
}
