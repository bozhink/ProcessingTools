// <copyright file="HandlebarsTemplateMetaDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Models.Mongo.Layout.Templates.Handlebars
{
    using System;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Templates;

    /// <summary>
    /// Handlebars template meta data transfer object (DTO).
    /// </summary>
    public class HandlebarsTemplateMetaDataTransferObject : IIdentifiedTemplateMetaDataTransferObject
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public Guid ObjectId { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }
    }
}
