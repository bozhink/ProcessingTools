// <copyright file="ContentTaggerSettings.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Layout
{
    using ProcessingTools.Models.Contracts.Processors.Layout;

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
