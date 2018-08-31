// <copyright file="ExternalLink.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Models.ExternalLinks
{
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Data.Miners.Models.Contracts.ExternalLinks;

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
