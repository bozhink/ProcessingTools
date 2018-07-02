// <copyright file="ObjectHistory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.History.Mongo
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Attributes;
    using ProcessingTools.Models.Contracts.History;

    /// <summary>
    /// Object history entity for MongoDB.
    /// </summary>
    [CollectionName("objectHistory")]
    public class ObjectHistory : IObjectHistory
    {
        /// <inheritdoc/>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Data { get; set; }

        /// <inheritdoc/>
        public string ObjectId { get; set; }

        /// <inheritdoc/>
        public string ObjectType { get; set; }

        /// <inheritdoc/>
        public string AssemblyName { get; set; }

        /// <inheritdoc/>
        public string AssemblyVersion { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }
    }
}
