// <copyright file="IContentTypeable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with content type.
    /// </summary>
    public interface IContentTypeable
    {
        /// <summary>
        /// Gets the content type.
        /// </summary>
        string ContentType { get; }
    }
}
