// <copyright file="IValidationCacheEntity.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Cache.Models
{
    using System;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Validation cache entity.
    /// </summary>
    public interface IValidationCacheEntity
    {
        /// <summary>
        /// Gets content.
        /// </summary>
        string Content { get; }

        /// <summary>
        /// Gets time of last update.
        /// </summary>
        DateTime LastUpdate { get; }

        /// <summary>
        /// Gets validation status.
        /// </summary>
        ValidationStatus Status { get; }
    }
}
