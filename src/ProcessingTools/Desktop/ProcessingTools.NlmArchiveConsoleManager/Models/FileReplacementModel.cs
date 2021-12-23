// <copyright file="FileReplacementModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.NlmArchiveConsoleManager.Models
{
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Models;

    /// <summary>
    /// File replacement model.
    /// </summary>
    public class FileReplacementModel : IFileReplacementModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileReplacementModel"/> class.
        /// </summary>
        /// <param name="destination">Destination of the files.</param>
        /// <param name="originalFileName">Original file name.</param>
        /// <param name="source">Source folder of the file.</param>
        public FileReplacementModel(string destination, string originalFileName, string source)
        {
            this.Destination = destination;
            this.OriginalFileName = originalFileName;
            this.Source = source;
        }

        /// <inheritdoc/>
        public string Destination { get; private set; }

        /// <inheritdoc/>
        public string OriginalFileName { get; private set; }

        /// <inheritdoc/>
        public string Source { get; private set; }
    }
}
