﻿// <copyright file="IContentTaggerSettings.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Contracts
{
    /// <summary>
    /// Content tagger settings.
    /// </summary>
    public interface IContentTaggerSettings
    {
        /// <summary>
        /// Gets a value indicating whether tagging should be case sensitive.
        /// </summary>
        bool CaseSensitive { get; }

        /// <summary>
        /// Gets a value indicating whether tagging should use minimal text selection.
        /// </summary>
        bool MinimalTextSelect { get; }
    }
}