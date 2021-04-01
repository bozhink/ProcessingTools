// <copyright file="IPermalink.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with permalink.
    /// </summary>
    public interface IPermalink
    {
        /// <summary>
        /// Gets the permalink.
        /// </summary>
        string Permalink { get; }
    }
}
