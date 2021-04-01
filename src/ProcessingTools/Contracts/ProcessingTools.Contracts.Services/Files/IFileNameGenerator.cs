// <copyright file="IFileNameGenerator.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Files
{
    /// <summary>
    /// File name generator.
    /// </summary>
    public interface IFileNameGenerator
    {
        /// <summary>
        /// Generates new file name based on the input fileName.
        /// </summary>
        /// <param name="fileName">Referent fileName for the new file name.</param>
        /// <returns>New file name.</returns>
        string GetNewFileName(string fileName);
    }
}
