// <copyright file="File.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Documents
{
    using System;
    using Common.Attributes;
    using Contracts.Documents.Files;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Models.Contracts.Documents.Documents;

    /// <summary>
    /// File
    /// </summary>
    [CollectionName("files")]
    public class File : IFileDetailsDataModel, IDocumentFileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="File"/> class.
        /// </summary>
        public File()
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
        public long ContentLength { get; set; }

        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public string FileExtension { get; set; }

        /// <inheritdoc/>
        public string FileName { get; set; }

        /// <inheritdoc/>
        public long OriginalContentLength { get; set; }

        /// <inheritdoc/>
        public string OriginalContentType { get; set; }

        /// <inheritdoc/>
        public string OriginalFileExtension { get; set; }

        /// <inheritdoc/>
        public string OriginalFileName { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfNull]
        public string SystemFileName { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }
    }
}
