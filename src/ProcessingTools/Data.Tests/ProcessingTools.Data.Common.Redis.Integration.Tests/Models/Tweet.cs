// <copyright file="Tweet.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Redis.Integration.Tests.Models
{
    using System;

    /// <summary>
    /// Tweet model.
    /// </summary>
    public class Tweet : ITweet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tweet"/> class.
        /// </summary>
        public Tweet()
        {
            this.PostedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the ID of the tweet.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the content of the tweet.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the date when the tweet is posted.
        /// </summary>
        public DateTime PostedOn { get; set; }
    }
}
