﻿// <copyright file="ICommentable.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with comment.
    /// </summary>
    public interface ICommentable
    {
        /// <summary>
        /// Gets the comment.
        /// </summary>
        string Comment { get; }
    }
}
