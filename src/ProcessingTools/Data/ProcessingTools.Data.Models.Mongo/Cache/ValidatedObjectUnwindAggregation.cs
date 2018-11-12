// <copyright file="ValidatedObjectUnwindAggregation.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Cache.Mongo
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// <see cref="ValidatedObject"/> version used in aggregation framework as unwind return type.
    /// </summary>
    public class ValidatedObjectUnwindAggregation
    {
        /// <summary>
        /// Gets or sets the key value.
        /// </summary>
        [BsonElement(nameof(ValidatedObjectProjectAggregation.Key))]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ValidationCacheEntity"/> value.
        /// </summary>
        [BsonElement(nameof(ValidatedObjectProjectAggregation.Values))]
        public ValidationCacheEntity Values { get; set; }
    }
}
