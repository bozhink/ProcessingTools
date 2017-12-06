// <copyright file="IModified.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    using System;

    /// <summary>
    /// Model with modification information.
    /// </summary>
    public interface IModified
    {
        /// <summary>
        /// Gets modifier.
        /// </summary>
        string ModifiedBy { get; }

        /// <summary>
        /// Gets date of modification.
        /// </summary>
        DateTime ModifiedOn { get; }
    }
}
