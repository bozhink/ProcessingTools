// <copyright file="Journal.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Documents.Mongo
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Data.Models.Contracts.Documents.Journals;

    /// <summary>
    /// Journal
    /// </summary>
    [CollectionName("journals")]
    public class Journal : IJournalDetailsDataModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Journal"/> class.
        /// </summary>
        public Journal()
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
        public string Name { get; set; }

        /// <inheritdoc/>
        public string AbbreviatedName { get; set; }

        /// <inheritdoc/>
        public string JournalId { get; set; }

        /// <inheritdoc/>
        public string PrintIssn { get; set; }

        /// <inheritdoc/>
        public string ElectronicIssn { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string PublisherId { get; set; }

        /// <inheritdoc/>
        public string JournalStyleId { get; set; }

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
        public IJournalPublisherDataModel Publisher { get; set; }

        /// <summary>
        /// Gets or sets the publisher from the database.
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonElement("publisher")]
        public Publisher DbPublisher { get; set; }

        /// <inheritdoc/>
        [BsonIgnore]
        public long NumberOfArticles { get; set; }
    }
}
