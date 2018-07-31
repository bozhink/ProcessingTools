// <copyright file="ValidationCacheEntity.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Cache.Redis
{
    using System;
    using ProcessingTools.Data.Models.Contracts.Cache;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Validation cache entity.
    /// </summary>
    public class ValidationCacheEntity : IValidationCacheDataModel
    {
        /// <inheritdoc/>
        public string Key { get; set; }

        /// <inheritdoc/>
        public string Content { get; set; }

        /// <inheritdoc/>
        public DateTime LastUpdate { get; set; }

        /// <inheritdoc/>
        public ValidationStatus Status { get; set; }
    }
}
