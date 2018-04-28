// <copyright file="JournalStyle.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Layout.Mongo
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Attributes;
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.Journals;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// Journal style.
    /// </summary>
    [CollectionName("journalStyles")]
    public class JournalStyle : IJournalStyleDataModel, IJournalDetailsStyleDataModel, ICreated, IModified
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalStyle"/> class.
        /// </summary>
        public JournalStyle()
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
        public IList<string> FloatObjectParseStyleIds { get; set; } = new List<string>();

        /// <inheritdoc/>
        public IList<string> FloatObjectTagStyleIds { get; set; } = new List<string>();

        /// <inheritdoc/>
        public IList<string> ReferenceParseStyleIds { get; set; } = new List<string>();

        /// <inheritdoc/>
        public IList<string> ReferenceTagStyleIds { get; set; } = new List<string>();

        /// <inheritdoc/>
        [BsonIgnore]
        public IList<IFloatObjectDetailsParseStyleModel> FloatObjectParseStyles { get; set; }

        /// <inheritdoc/>
        [BsonIgnore]
        public IList<IFloatObjectDetailsTagStyleModel> FloatObjectTagStyles { get; set; }

        /// <inheritdoc/>
        [BsonIgnore]
        public IList<IReferenceDetailsParseStyleModel> ReferenceParseStyles { get; set; }

        /// <inheritdoc/>
        [BsonIgnore]
        public IList<IReferenceDetailsTagStyleModel> ReferenceTagStyles { get; set; }

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
