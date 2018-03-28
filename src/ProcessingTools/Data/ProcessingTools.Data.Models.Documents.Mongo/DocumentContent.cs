// <copyright file="DocumentContent.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Documents.Mongo
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Data.Models.Contracts.Documents.Documents;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Document content.
    /// </summary>
    [CollectionName("documents.content")]
    public class DocumentContent : IDocumentContentDataModel, IStringIdentifiable, IModified
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
