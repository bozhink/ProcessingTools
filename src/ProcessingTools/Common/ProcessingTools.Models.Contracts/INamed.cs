// <copyright file="INamed.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
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
