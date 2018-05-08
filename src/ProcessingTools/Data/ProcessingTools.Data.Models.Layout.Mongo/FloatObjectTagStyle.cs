// <copyright file="FloatObjectTagStyle.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Layout.Mongo
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Attributes;
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Float object tag style.
    /// </summary>
    [CollectionName("floatObjectTagStyles")]
    public class FloatObjectTagStyle : IFloatObjectTagStyleDataModel, IFloatObjectDetailsTagStyleDataModel, ICreated, IModified
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectTagStyle"/> class.
        /// </summary>
        public FloatObjectTagStyle()
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
        public ReferenceType FloatReferenceType { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string FloatTypeNameInLabel { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string MatchCitationPattern { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string InternalReferenceType { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string ResultantReferenceType { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string FloatObjectXPath { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string TargetXPath { get; set; } = "./*";

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
