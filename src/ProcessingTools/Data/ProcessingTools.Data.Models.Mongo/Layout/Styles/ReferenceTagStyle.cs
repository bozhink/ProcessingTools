// <copyright file="ReferenceTagStyle.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Layout.Styles
{
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Contracts.Data.Models.Layout.Styles;

    /// <summary>
    /// Reference tag style.
    /// </summary>
    [CollectionName("referenceTagStyles")]
    public class ReferenceTagStyle : MongoDataModel, IReferenceTagStyleDataModel
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
        public string TargetXPath { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string Script { get; set; }
    }
}
