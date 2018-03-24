// <copyright file="IFileWriter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.IO
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// File writer.
    /// </summary>
    public interface IFileWriter
    {
        /// <summary>
        /// Writes a stream under specified file name.
        /// </summary>
        /// <param name="fileName">Full name of the output file.</param>
        /// <param name="stream">Stream to be written.</param>
        /// <returns>Task</returns>
        Task<object> WriteAsync(string fileName, Stream stream);

        /// <summary>
        /// Writes stream under <see cref="StreamReader"/> object under specified file name.
        /// </summary>
        /// <param name="fileName">Full name of the output file.</param>
        /// <param name="streamReader"><see cref="StreamReader"/> to be read.</param>
        /// <returns>Task</returns>
        Task<object> WriteAsync(string fileName, StreamReader streamReader);
    }
}
