// <copyright file="ITweet.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Memory.Unit.Tests.Models
{
    using System;

    /// <summary>
    /// Tweet
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
        /// Gets the date of creation of the tweet.
        /// </summary>
        DateTime PostedOn { get; }
    }
}
