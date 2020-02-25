﻿// <copyright file="ITweet.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Redis.Integration.Tests.Models
{
    using System;

    /// <summary>
    /// Tweet model.
    /// </summary>
    public interface ITweet
    {
        /// <summary>
        /// Gets the ID of the tweet.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the content of the tweet.
        /// </summary>
        string Content { get; }

        /// <summary>
        /// Gets the date when the tweet is posted.
        /// </summary>
        DateTime PostedOn { get; }
    }
}
