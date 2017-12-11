// <copyright file="INameable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with name.
    /// </summary>
    public interface INameable
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
    }
}
