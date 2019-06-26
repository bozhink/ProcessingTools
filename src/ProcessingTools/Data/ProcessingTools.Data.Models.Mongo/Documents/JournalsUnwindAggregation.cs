﻿// <copyright file="JournalsUnwindAggregation.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Documents
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Journals unwind aggregation: { Journals: { ... } }.
    /// </summary>
    public class JournalsUnwindAggregation
    {
        /// <summary>
        /// Gets or sets the Journals property in the unwind stage.
        /// </summary>
        [BsonElement(nameof(JournalsProjectAggregation.Journals))]
        public Journal Journals { get; set; }
    }
}
