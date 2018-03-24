// <copyright file="Publisher.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Documents.Mongo
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Data.Models.Contracts.Documents.Publishers;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Publisher
    /// </summary>
    [CollectionName("publishers")]
    public class Publisher : IPublisherDataModel, IPublisherDetailsDataModel, ICreated, IModified
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> class.
        /// </summary>
        public Publisher()
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
        public string Address { get; set; }

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
        public long NumberOfJournals { get; set; }
    }
}
