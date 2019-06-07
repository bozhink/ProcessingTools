// <copyright file="IDescribed.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with description.
    /// </summary>
    public interface IDescribed
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        string Description { get; }
    }
}
