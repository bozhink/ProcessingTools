// <copyright file="ContentTaggerSettings.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models
{
    using ProcessingTools.Services.Models.Contracts;

    /// <summary>
    /// Content tagger settings.
    /// </summary>
    public class ContentTaggerSettings : IContentTaggerSettings
    {
        /// <inheritdoc/>
        public bool CaseSensitive { get; set; } = true;

        /// <inheritdoc/>
        public bool MinimalTextSelect { get; set; } = false;
    }
}
