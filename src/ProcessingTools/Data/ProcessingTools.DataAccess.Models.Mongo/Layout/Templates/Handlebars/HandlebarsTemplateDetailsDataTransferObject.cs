// <copyright file="HandlebarsTemplateDetailsDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Models.Mongo.Layout.Templates.Handlebars
{
    using System;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Templates.Handlebars;

    /// <summary>
    /// Handlebars template details data transfer object (DTO).
    /// </summary>
    public class HandlebarsTemplateDetailsDataTransferObject : IHandlebarsTemplateDetailsDataTransferObject
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public Guid ObjectId { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public string Content { get; set; }

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
