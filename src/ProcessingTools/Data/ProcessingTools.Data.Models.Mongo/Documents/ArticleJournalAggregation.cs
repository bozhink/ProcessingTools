// <copyright file="ArticleJournalAggregation.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Documents
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Article-Journal aggregation.
    /// </summary>
    public class ArticleJournalAggregation
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public Guid ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the journal object ID.
        /// </summary>
        public string JournalId { get; set; }

        /// <summary>
        /// Gets or sets the aggregated journal by lookup.
        /// </summary>
        public IEnumerable<Journal> Journals { get; set; }
    }
}
