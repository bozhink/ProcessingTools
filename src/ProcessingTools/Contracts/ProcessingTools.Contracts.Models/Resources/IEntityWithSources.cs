// <copyright file="IEntityWithSources.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Resources
{
    using System.Collections.Generic;

    /// <summary>
    /// Model with sources.
    /// </summary>
    public interface IEntityWithSources
    {
        /// <summary>
        /// Gets sources.
        /// </summary>
        IEnumerable<ISourceIdEntity> Sources { get; }
    }
}
