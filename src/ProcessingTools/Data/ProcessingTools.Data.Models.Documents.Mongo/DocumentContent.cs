// <copyright file="DocumentContent.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Documents.Mongo
{
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Data.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Document content.
    /// </summary>
    public class DocumentContent : IDocumentContentDataModel
    {
        /// <inheritdoc/>
        [BsonRequired]
        public string DocumentId { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string Content { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string ContentType { get; set; }
    }
}
