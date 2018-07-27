// <copyright file="IValidationCacheDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Cache
{
    using System.Threading.Tasks;
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
        /// <param name="model">Model item to be added to key.</param>
        /// <returns>Task of result.</returns>
        Task<object> AddAsync(string key, IValidationCacheModel model);

        /// <summary>
        /// Gets the last cache item registered for specified key.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns>The last cache item registered for the specified key</returns>
        Task<IValidationCacheModel> GetLastForKeyAsync(string key);

        /// <summary>
        /// Gets all the cache items registered for specified key.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns>All the cache items registered for specified key.</returns>
        Task<IValidationCacheModel[]> GetAllForKeyAsync(string key);
    }
}
