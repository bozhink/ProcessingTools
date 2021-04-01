// <copyright file="TemplateMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Templates
{
    using ProcessingTools.Contracts.Models.Layout.Templates;

    /// <summary>
    /// Template model.
    /// </summary>
    public class TemplateMetaModel : IIdentifiedTemplateMetaModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }
    }
}
