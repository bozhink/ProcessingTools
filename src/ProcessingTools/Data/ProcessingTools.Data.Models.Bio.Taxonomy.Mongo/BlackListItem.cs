// <copyright file="BlackListItem.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Attributes;
    using ProcessingTools.Data.Models.Contracts.Bio.Taxonomy;

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
