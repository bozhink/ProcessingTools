// <copyright file="DocumentContent.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Documents
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Documents;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document content.
    /// </summary>
    [CollectionName("documents.content")]
    public class DocumentContent : IDocumentContentDataTransferObject, IStringIdentified, IModified
    {
        /// <inheritdoc/>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string DocumentId { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string Content { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }
    }
}
