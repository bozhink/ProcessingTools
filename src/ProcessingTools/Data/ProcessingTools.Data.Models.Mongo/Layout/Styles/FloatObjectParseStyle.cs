// <copyright file="FloatObjectParseStyle.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Layout.Styles
{
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Contracts.Data.Models.Layout.Styles;

    /// <summary>
    /// Float object parse style.
    /// </summary>
    [CollectionName("floatObjectParseStyles")]
    public class FloatObjectParseStyle : MongoDataModel, IFloatObjectParseStyleDataModel
    {
        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string FloatObjectXPath { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string Script { get; set; }
    }
}
