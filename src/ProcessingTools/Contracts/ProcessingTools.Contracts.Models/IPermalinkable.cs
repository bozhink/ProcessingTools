// <copyright file="IPermalinkable.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with permalink.
    /// </summary>
    public interface IPermalinkable
    {
        /// <summary>
        /// Gets the permalink.
        /// </summary>
        string Permalink { get; }
    }
}
