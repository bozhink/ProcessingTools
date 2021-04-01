// <copyright file="IFileNameResolver.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Files
{
    /// <summary>
    /// File name resolver.
    /// </summary>
    public interface IFileNameResolver
    {
        /// <summary>
        /// Resolves the full file name of a specified file.
        /// </summary>
        /// <param name="fileName">File name to be resolved.</param>
        /// <returns>Full file name path of the specified file.</returns>
        string GetFullFileName(string fileName);
    }
}
