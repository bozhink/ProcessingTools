// <copyright file="ICreated.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    using System;

    /// <summary>
    /// Model with creation information.
    /// </summary>
    public interface ICreated
    {
        /// <summary>
        /// Gets creator.
        /// </summary>
        string CreatedBy { get; }

        /// <summary>
        /// Gets date of creation.
        /// </summary>
        DateTime CreatedOn { get; }
    }
}
