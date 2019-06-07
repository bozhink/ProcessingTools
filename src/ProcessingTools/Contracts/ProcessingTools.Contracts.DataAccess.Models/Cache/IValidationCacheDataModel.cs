// <copyright file="IValidationCacheDataModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Cache
{
    using ProcessingTools.Contracts.Models.Cache;

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
