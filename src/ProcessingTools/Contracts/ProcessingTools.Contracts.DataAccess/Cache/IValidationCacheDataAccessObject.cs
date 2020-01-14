// <copyright file="IValidationCacheDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Cache
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Models.Cache;
    using ProcessingTools.Contracts.Models.Cache;

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
        Task<bool> AddAsync(string key, IValidationCacheModel model);

        /// <summary>
        /// Removes item from specified key.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <param name="model">>Model item to be removed from the key.</param>
        /// <returns>Task of result.</returns>
        Task<bool> RemoveAsync(string key, IValidationCacheModel model);

        /// <summary>
        /// Removes entire key with all data under it.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns>Task of result.</returns>
        Task<bool> RemoveAsync(string key);

        /// <summary>
        /// Clear the cache store. Removes all keys and all the data under them.
        /// </summary>
        /// <returns>Task of result.</returns>
        Task<bool> ClearCacheAsync();

        /// <summary>
        /// Gets the last cache item registered for specified key.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns>The last cache item registered for the specified key.</returns>
        Task<IValidationCacheDataTransferObject> GetLastForKeyAsync(string key);

        /// <summary>
        /// Gets all the cache items registered for specified key.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns>All the cache items registered for specified key.</returns>
        Task<IList<IValidationCacheDataTransferObject>> GetAllForKeyAsync(string key);
    }
}
