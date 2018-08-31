// <copyright file="IValidationCacheModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Cache
{
    using System;
    using ProcessingTools.Common.Enumerations;

    /// <summary>
    /// Validation cache entity.
    /// </summary>
    public interface IValidationCacheModel
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
