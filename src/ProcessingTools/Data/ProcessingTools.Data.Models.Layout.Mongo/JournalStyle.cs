// <copyright file="JournalStyle.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Layout.Mongo
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.Journals;
    using ProcessingTools.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Models.Contracts.Layout.Styles.Journals;
    using ProcessingTools.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// Journal style.
    /// </summary>
    [CollectionName("journalStyles")]
    public class JournalStyle : IJournalDetailsStyleDataModel
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

        /// <summary>
        /// Gets or sets the float object parse styles.
        /// </summary>
        [BsonIgnoreIfDefault]
        public IList<FloatObjectParseStyle> FloatObjectParseStyles { get; set; }

        /// <summary>
        /// Gets or sets the float object tag styles.
        /// </summary>
        [BsonIgnoreIfDefault]
        public IList<FloatObjectTagStyle> FloatObjectTagStyles { get; set; }

        /// <summary>
        /// Gets or sets the reference parse styles.
        /// </summary>
        [BsonIgnoreIfDefault]
        public IList<ReferenceParseStyle> ReferenceParseStyles { get; set; }

        /// <summary>
        /// Gets or sets the reference tag styles.
        /// </summary>
        [BsonIgnoreIfDefault]
        public IList<ReferenceTagStyle> ReferenceTagStyles { get; set; }

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
        IEnumerable<IFloatObjectDetailsParseStyleModel> IJournalDetailsStyleModel.FloatObjectParseStyles => this.FloatObjectParseStyles;

        /// <inheritdoc/>
        [BsonIgnore]
        IEnumerable<IFloatObjectDetailsTagStyleModel> IJournalDetailsStyleModel.FloatObjectTagStyles => this.FloatObjectTagStyles;

        /// <inheritdoc/>
        [BsonIgnore]
        IEnumerable<IReferenceDetailsParseStyleModel> IJournalDetailsStyleModel.ReferenceParseStyles => this.ReferenceParseStyles;

        /// <inheritdoc/>
        [BsonIgnore]
        IEnumerable<IReferenceDetailsTagStyleModel> IJournalDetailsStyleModel.ReferenceTagStyles => this.ReferenceTagStyles;
    }
}
