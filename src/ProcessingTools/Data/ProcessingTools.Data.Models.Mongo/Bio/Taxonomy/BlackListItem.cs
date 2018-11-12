// <copyright file="BlackListItem.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Bio.Taxonomy
{
    using Common.Attributes;
    using Contracts.Bio.Taxonomy;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

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
