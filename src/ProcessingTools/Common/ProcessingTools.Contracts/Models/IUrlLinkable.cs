// <copyright file="IUrlLinkable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with URL.
    /// </summary>
    public interface IUrlLinkable
    {
        /// <summary>
        /// Gets the URL.
        /// </summary>
        string Url { get; }
    }
}
