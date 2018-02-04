// <copyright file="ValidationCacheServiceModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Cache
{
    using System;
    using ProcessingTools.Models.Contracts.Cache;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Validation cache service model.
    /// </summary>
    public class ValidationCacheServiceModel : IValidationCacheModel
    {
        /// <summary>
        /// Gets or sets the content of the cache item.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the last update date of the cache item.
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Gets or sets the validation status.
        /// </summary>
        public ValidationStatus Status { get; set; }
    }
}
