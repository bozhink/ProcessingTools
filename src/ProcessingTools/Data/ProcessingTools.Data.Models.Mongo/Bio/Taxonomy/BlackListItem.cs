﻿// <copyright file="BlackListItem.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Bio.Taxonomy
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Contracts.DataAccess.Models.Bio.Taxonomy;

    /// <summary>
    /// MongoDB implementation of <see cref="IBlackListItem"/>.
    /// </summary>
    [CollectionName("blackList")]
    public class BlackListItem : IBlackListItem
    {
        /// <summary>
        /// Gets or sets the _id.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the content of the item.
        /// </summary>
        [BsonRepresentation(BsonType.String)]
        public string Content { get; set; }
    }
}
