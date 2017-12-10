// <copyright file="IExternalLinkModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Harvesters.ExternalLinks
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// External link.
    /// </summary>
    public interface IExternalLinkModel : IValuable, IFullAddressable
    {
        /// <summary>
        /// Gets the base address.
        /// </summary>
        string BaseAddress { get; }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        string Uri { get; }
    }
}
