// <copyright file="FileNameResolver.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Files
{
    using System.IO;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Services.Contracts.Files;

    /// <summary>
    /// File name resolver.
    /// </summary>
    public class FileNameResolver : IFileNameResolver
    {
        /// <summary>
        /// Gets or sets the base directory name.
        /// </summary>
        public string BaseDirectoryName { get; set; }

        /// <inheritdoc/>
        public string GetFullFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            if (!string.IsNullOrWhiteSpace(this.BaseDirectoryName))
            {
                return Path.Combine(this.BaseDirectoryName, fileName);
            }

            return fileName;
        }
    }
}
