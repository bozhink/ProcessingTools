﻿// <copyright file="IValidationCacheService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Cache
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Cache;

    /// <summary>
    /// Validation cache service.
    /// </summary>
    public interface IValidationCacheService
    {
        /// <summary>
        /// Adds key-value pair to cache.
        /// </summary>
        /// <param name="key">Key string for the cache item.</param>
        /// <param name="value"><see cref="IValidationCacheModel"/> item to be cached.</param>
        /// <returns>Task.</returns>
        Task<object> AddAsync(string key, IValidationCacheModel value);

        /// <summary>
        /// Gets cached item by key.
        /// </summary>
        /// <param name="key">Key string for the cache item.</param>
        /// <returns>Task of cached item.</returns>
        Task<IValidationCacheModel> GetAsync(string key);
    }
}
