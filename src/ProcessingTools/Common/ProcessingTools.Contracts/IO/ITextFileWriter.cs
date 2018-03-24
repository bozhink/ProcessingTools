// <copyright file="ITextFileWriter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.IO
{
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// File writer for text files.
    /// </summary>
    public interface ITextFileWriter
    {
        /// <summary>
        /// Gets or sets the encoding of the output file.
        /// </summary>
        Encoding Encoding { get; set; }

        /// <summary>
        /// Writes text to a file with specified name.
        /// </summary>
        /// <param name="fileName">Full name of the output file.</param>
        /// <param name="text">Text to be  written.</param>
        /// <returns>Task</returns>
        Task<object> WriteAsync(string fileName, string text);
    }
}
