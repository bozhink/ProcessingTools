// <copyright file="INamed.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with name.
    /// </summary>
    public interface INamed
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
    }
}
