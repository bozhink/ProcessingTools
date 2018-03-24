// <copyright file="IAbbreviatedNameable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    /// <summary>
    /// Model with abbreviated name.
    /// </summary>
    public interface IAbbreviatedNameable
    {
        /// <summary>
        /// Gets the abbreviated name.
        /// </summary>
        string AbbreviatedName { get; }
    }
}
