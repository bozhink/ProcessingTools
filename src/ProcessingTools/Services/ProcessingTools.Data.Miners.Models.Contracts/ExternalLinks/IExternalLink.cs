// <copyright file="IExternalLink.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Models.Contracts.ExternalLinks
{
    using ProcessingTools.Common.Enumerations.Nlm;

    /// <summary>
    /// External link.
    /// </summary>
    public interface IExternalLink
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        string Content { get; }

        /// <summary>
        /// Gets the href.
        /// </summary>
        string Href { get; }

        /// <summary>
        /// Gets the external link type.
        /// </summary>
        ExternalLinkType Type { get; }
    }
}
