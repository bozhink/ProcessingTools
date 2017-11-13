// <copyright file="IAbbreviation.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Contracts.Abbreviations
{
    /// <summary>
    /// Abbreviation.
    /// </summary>
    public interface IAbbreviation
    {
        /// <summary>
        /// Gets content.
        /// </summary>
        string Content { get; }

        /// <summary>
        /// Gets content-type.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Gets definition.
        /// </summary>
        string Definition { get; }

        /// <summary>
        /// Gets replace pattern.
        /// </summary>
        string ReplacePattern { get; }

        /// <summary>
        /// Gets search pattern.
        /// </summary>
        string SearchPattern { get; }
    }
}
