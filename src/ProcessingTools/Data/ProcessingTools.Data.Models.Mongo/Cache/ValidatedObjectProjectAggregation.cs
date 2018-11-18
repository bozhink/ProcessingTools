// <copyright file="ValidatedObjectProjectAggregation.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Cache
{
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// <see cref="ValidatedObject"/> version used in aggregation framework as projection return type.
    /// </summary>
    public class ValidatedObjectProjectAggregation
    {
        /// <summary>
        /// Gets or sets the key value.
        /// </summary>
        [BsonElement(nameof(ValidatedObjectProjectAggregation.Key))]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets values.
        /// </summary>
        [BsonElement(nameof(ValidatedObjectProjectAggregation.Values))]
        public IEnumerable<ValidationCacheEntity> Values { get; set; }
    }
}
