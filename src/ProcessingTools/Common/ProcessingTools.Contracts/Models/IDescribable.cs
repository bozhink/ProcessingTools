// <copyright file="IDescribable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    /// <summary>
    /// Model with description.
    /// </summary>
    public interface IDescribable
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        string Description { get; }
    }
}
