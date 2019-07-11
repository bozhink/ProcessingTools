// <copyright file="FileNameGeneratorWithUniqueNaming.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Files
{
    using System;
    using System.IO;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Files;

    /// <summary>
    /// File name generator with unique naming.
    /// </summary>
    public class FileNameGeneratorWithUniqueNaming : IFileNameGenerator
    {
        /// <inheritdoc/>
        public string GetNewFileName(string fileName)
        {
            string extension;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                extension = null;
            }
            else
            {
                extension = Path.GetExtension(fileName);
            }

            string newFileName = $"{DateTime.UtcNow:yyyyMMddHHmmssffffff}-{Guid.NewGuid()}{extension}";

            return newFileName.Substring(0, Math.Min(newFileName.Length, FileConstants.MaximalLengthOfGeneratedNewFileName));

            throw new NotImplementedException();
        }
    }
}
