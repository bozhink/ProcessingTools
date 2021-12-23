// <copyright file="IContentTyped.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with content type.
    /// </summary>
    public interface IContentTyped
    {
        /// <summary>
        /// Gets the content type.
        /// </summary>
        string ContentType { get; }
    }
}
