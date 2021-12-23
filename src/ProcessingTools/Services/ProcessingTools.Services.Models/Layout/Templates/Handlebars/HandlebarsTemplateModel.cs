// <copyright file="HandlebarsTemplateModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Templates.Handlebars
{
    using ProcessingTools.Contracts.Models.Layout.Templates.Handlebars;

    /// <summary>
    /// Handlebars template model.
    /// </summary>
    public class HandlebarsTemplateModel : IHandlebarsTemplateModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public string Content { get; set; }
    }
}
