// <copyright file="ReferenceParseStyle.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Layout.Styles
{
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Contracts.Data.Models.Layout.Styles;

    /// <summary>
    /// Reference parse style.
    /// </summary>
    [CollectionName("referenceParseStyles")]
    public class ReferenceParseStyle : MongoDataModel, IReferenceParseStyleDataModel
    {
        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string ReferenceXPath { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string Script { get; set; }
    }
}
