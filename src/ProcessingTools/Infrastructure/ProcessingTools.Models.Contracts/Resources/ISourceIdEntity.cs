// <copyright file="ISourceIdEntity.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Resources
{
    /// <summary>
    /// Entity with source ID.
    /// </summary>
    public interface ISourceIdEntity
    {
        /// <summary>
        /// Gets source ID.
        /// </summary>
        string SourceId { get; }
    }
}
