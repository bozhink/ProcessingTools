// <copyright file="FloatObjectTagStyle.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Layout.Styles
{
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Contracts.Data.Models.Layout.Styles;

    /// <summary>
    /// Float object tag style.
    /// </summary>
    [CollectionName("floatObjectTagStyles")]
    public class FloatObjectTagStyle : MongoDataModel, IFloatObjectTagStyleDataModel
    {
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
    }
}
