﻿// <copyright file="IExternalLinkModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.Contracts.ExternalLinks
{
    /// <summary>
    /// External link.
    /// </summary>
    public interface IExternalLinkModel
    {
        /// <summary>
        /// Gets the base address.
        /// </summary>
        string BaseAddress { get; }

        /// <summary>
        /// Gets the full address.
        /// </summary>
        string FullAddress { get; }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        string Uri { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        string Value { get; }
    }
}