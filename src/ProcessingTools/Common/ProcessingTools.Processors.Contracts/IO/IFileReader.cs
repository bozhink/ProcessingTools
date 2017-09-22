// <copyright file="IFileReader.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.IO
{
    using System.IO;

    /// <summary>
    /// File reader.
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Gets a <see cref="StreamReader"/> based on specified file name.
        /// </summary>
        /// <param name="fullName">Full name of the file to be read.</param>
        /// <returns><see cref="StreamReader"/> object.</returns>
        StreamReader GetReader(string fullName);

        /// <summary>
        /// Reads a file to <see cref="Stream"/>.
        /// </summary>
        /// <param name="fullName">Full name of the file to be read.</param>
        /// <returns><see cref="Stream"/> object.</returns>
        Stream ReadToStream(string fullName);
    }
}
