// <copyright file="ISourceIdEntity.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.DataResources.Models
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
