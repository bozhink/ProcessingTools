// <copyright file="ReferenceTagStyle.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Layout.Mongo
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// Reference tag style.
    /// </summary>
    [CollectionName("referenceTagStyles")]
    public class ReferenceTagStyle : IReferenceDetailsTagStyleDataModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTagStyle"/> class.
        /// </summary>
        public ReferenceTagStyle()
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
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string ReferenceXPath { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string TargetXPath { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string Script { get; set; }

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
