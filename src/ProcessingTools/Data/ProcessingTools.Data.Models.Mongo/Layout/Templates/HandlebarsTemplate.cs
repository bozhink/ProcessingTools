// <copyright file="HandlebarsTemplate.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Layout.Templates
{
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Contracts.Data.Models.Layout.Templates;

    /// <summary>
    /// Handlebars template.
    /// </summary>
    [CollectionName("handlebarsTemplates")]
    public class HandlebarsTemplate : MongoDataModel, IHandlebarsTemplateDataModel
    {
        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        [BsonRequired]
        public string Content { get; set; }
    }
}
