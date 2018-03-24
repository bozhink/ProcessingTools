// <copyright file="IStreamingFileContentDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Files
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Streaming file content data service.
    /// </summary>
    public interface IStreamingFileContentDataService
    {
        /// <summary>
        /// Gets <see cref="StreamReader"/> of a file.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <returns><see cref="StreamReader"/> for the file content.</returns>
        StreamReader GetReader(object id);

        /// <summary>
        /// Reads a file to a stream.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <returns><see cref="Stream"/> of the file content.</returns>
        Stream ReadToStream(object id);

        /// <summary>
        /// Writes stream to a file.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <param name="stream"><see cref="Stream"/> of the file content.</param>
        /// <returns>Task</returns>
        Task<object> WriteAsync(object id, Stream stream);

        /// <summary>
        /// Writes content of a <see cref="StreamReader"/> to a file.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <param name="streamReader"><see cref="StreamReader"/> for the file content</param>
        /// <returns>Task</returns>
        Task<object> WriteAsync(object id, StreamReader streamReader);
    }
}
