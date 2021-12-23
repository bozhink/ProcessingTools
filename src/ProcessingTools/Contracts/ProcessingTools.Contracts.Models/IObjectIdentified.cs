// <copyright file="IObjectIdentified.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    using System;

    /// <summary>
    /// Model with object-ID.
    /// </summary>
    public interface IObjectIdentified
    {
        /// <summary>
        /// Gets the object ID.
        /// </summary>
        Guid ObjectId { get; }
    }
}
