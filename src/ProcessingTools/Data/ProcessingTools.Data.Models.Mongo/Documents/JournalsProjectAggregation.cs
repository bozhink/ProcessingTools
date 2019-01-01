﻿// <copyright file="JournalsProjectAggregation.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Documents
{
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Journals project aggregation: { Journals: [ ... ] }
    /// </summary>
    public class JournalsProjectAggregation
    {
        /// <summary>
        /// Gets or sets the journals.
        /// </summary>
        [BsonElement(nameof(JournalsProjectAggregation.Journals))]
        public IEnumerable<Journal> Journals { get; set; }
    }
}
