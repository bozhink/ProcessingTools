﻿// <copyright file="IMediaTypeable.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    /// <summary>
    /// Model with media-type.
    /// </summary>
    public interface IMediaTypeable
    {
        /// <summary>
        /// Gets the media-type.
        /// </summary>
        string MediaType { get; }
    }
}
