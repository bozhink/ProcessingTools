// <copyright file="IValidationCacheDataModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Contracts.Cache
{
    using ProcessingTools.Models.Contracts.Cache;

    /// <summary>
    /// Validation cache data model.
    /// </summary>
    public interface IValidationCacheDataModel : IValidationCacheModel
    {
        /// <summary>
        /// Gets the cache key.
        /// </summary>
        string Key { get; }
    }
}
