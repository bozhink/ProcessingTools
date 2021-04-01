// <copyright file="IFileOverwritable.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// File overwritable.
    /// </summary>
    public interface IFileOverwritable
    {
        /// <summary>
        /// Gets or sets a value indicating whether files have to be overwritten.
        /// </summary>
        bool OverwriteFile { get; set; }
    }
}
