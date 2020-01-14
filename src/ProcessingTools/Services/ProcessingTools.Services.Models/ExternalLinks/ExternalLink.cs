// <copyright file="ExternalLink.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.ExternalLinks
{
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Contracts.Models.ExternalLinks;

    /// <summary>
    /// External link.
    /// </summary>
    public class ExternalLink : IExternalLink
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the external link type.
        /// </summary>
        public ExternalLinkType Type { get; set; }
    }
}
