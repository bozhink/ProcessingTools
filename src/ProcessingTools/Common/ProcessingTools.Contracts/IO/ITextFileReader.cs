// <copyright file="ITextFileReader.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.IO
{
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// File reader for text files.
    /// </summary>
    public interface ITextFileReader
    {
        /// <summary>
        /// Gets or sets the encoding of the output file.
        /// </summary>
        Encoding Encoding { get; set; }

        /// <summary>
        /// Reads all content of a text file.
        /// </summary>
        /// <param name="fileName">Full name of the file to be read.</param>
        /// <returns>Task of the file content.</returns>
        Task<string> ReadAllTextAsync(string fileName);
    }
}
