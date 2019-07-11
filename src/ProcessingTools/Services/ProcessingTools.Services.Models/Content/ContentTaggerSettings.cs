// <copyright file="ContentTaggerSettings.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Content
{
    using ProcessingTools.Contracts.Services.Models.Content;

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
