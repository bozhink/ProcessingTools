// <copyright file="IExternalLinkModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.Contracts.ExternalLinks
{
    using ProcessingTools.Models.Contracts;

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
