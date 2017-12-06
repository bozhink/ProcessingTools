// <copyright file="IFileNameable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with file name.
    /// </summary>
    public interface IFileNameable
    {
        /// <summary>
        /// Gets the file name.
        /// </summary>
        string FileName { get; }
    }
}
