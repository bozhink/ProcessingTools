// <copyright file="IFileNameable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
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
