// <copyright file="IComment.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with comment.
    /// </summary>
    public interface IComment
    {
        /// <summary>
        /// Gets the comment.
        /// </summary>
        string Comment { get; }
    }
}
