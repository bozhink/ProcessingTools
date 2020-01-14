// <copyright file="JournalStyle.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Layout.Styles
{
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Contracts.Data.Models.Layout.Styles;

    /// <summary>
    /// Journal style.
    /// </summary>
    [CollectionName("journalStyles")]
    public class JournalStyle : MongoDataModel, IJournalStyleDataModel
    {
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
    }
}
