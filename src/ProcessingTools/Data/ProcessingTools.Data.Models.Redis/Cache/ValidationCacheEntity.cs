// <copyright file="ValidationCacheEntity.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Redis.Cache
{
    using System;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.DataAccess.Models.Cache;
    using ProcessingTools.Contracts.Models.Cache;

    /// <summary>
    /// Validation cache entity.
    /// </summary>
    public class ValidationCacheEntity : IValidationCacheDataTransferObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCacheEntity"/> class.
        /// </summary>
        public ValidationCacheEntity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCacheEntity"/> class.
        /// </summary>
        /// <param name="entity">Entity yo be used for initialization.</param>
        public ValidationCacheEntity(IValidationCacheModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Content = entity.Content;
            this.LastUpdate = entity.LastUpdate;
            this.Status = entity.Status;
        }

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
