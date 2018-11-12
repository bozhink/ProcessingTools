// <copyright file="Mediatype.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Files
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Data.Models.Contracts.Files.Mediatypes;

    /// <summary>
    /// Mediatype
    /// </summary>
    [CollectionName("mediatypes")]
    public class Mediatype : IMediatypeDetailsDataModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mediatype"/> class.
        /// </summary>
        public Mediatype()
        {
            this.ObjectId = Guid.NewGuid();
        }

        /// <inheritdoc/>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public Guid ObjectId { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string Extension { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string MimeType { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string MimeSubtype { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }

        /// <inheritdoc/>
        [BsonIgnore]
        public string ContentType => $"{this.MimeType}/{this.MimeSubtype}";
    }
}
