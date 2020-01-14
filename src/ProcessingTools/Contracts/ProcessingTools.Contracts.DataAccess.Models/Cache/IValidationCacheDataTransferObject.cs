// <copyright file="IValidationCacheDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Cache
{
    using ProcessingTools.Contracts.Models.Cache;

    /// <summary>
    /// Validation cache data transfer object (DTO).
    /// </summary>
    public interface IValidationCacheDataTransferObject : IValidationCacheModel
    {
        /// <summary>
        /// Gets the cache key.
        /// </summary>
        string Key { get; }
    }
}
