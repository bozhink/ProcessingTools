// <copyright file="ValidatedObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Cache
{
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Validated object.
    /// </summary>
    public class ValidatedObject : IStringIdentifiable
    {
        /// <summary>
        /// Gets or sets the _id field value.
        /// </summary>
        [BsonId]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the collection of values.
        /// </summary>
        public ICollection<ValidationCacheEntity> Values { get; set; } = new List<ValidationCacheEntity>();
    }
}
