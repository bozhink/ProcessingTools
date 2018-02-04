// <copyright file="ICommentable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
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
