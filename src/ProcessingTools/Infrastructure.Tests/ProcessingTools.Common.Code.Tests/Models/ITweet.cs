// <copyright file="ITweet.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Tests.Models
{
    using System;

    /// <summary>
    /// Tweet model.
    /// </summary>
    public interface ITweet
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Gets or sets the publication date.
        /// </summary>
        DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the number of faves.
        /// </summary>
        int Faves { get; set; }
    }
}
