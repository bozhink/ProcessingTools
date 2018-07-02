// <copyright file="IStreamingFilesDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Files
{
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Files;

    /// <summary>
    /// Streaming files data service.
    /// </summary>
    public interface IStreamingFilesDataService
    {
        /// <summary>
        /// Creates a file by file metadata and stream.
        /// </summary>
        /// <param name="metadata">Metadata of the file.</param>
        /// <param name="stream">Stream of the content of the file.</param>
        /// <returns>Task of the file metadata.</returns>
        Task<IFileMetadata> CreateAsync(IFileMetadata metadata, Stream stream);

        /// <summary>
        /// Deletes a file by ID.
        /// </summary>
        /// <param name="id">ID of the file to be deleted.</param>
        /// <returns>Task of deletion status.</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Gets metadata of a file by ID.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <returns>Task of file metadata.</returns>
        Task<IFileMetadata> GetMetadataAsync(object id);

        /// <summary>
        /// Gets stream reader of a file by ID.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <returns><see cref="StreamReader"/> of the file.</returns>
        StreamReader GetReader(object id);

        /// <summary>
        /// Reads a file to a stream.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <returns><see cref="Stream"/> of the file.</returns>
        Stream ReadToStream(object id);

        /// <summary>
        /// Updates file content by ID.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <param name="stream">Stream of the file.</param>
        /// <returns>Task of file metadata.</returns>
        Task<IFileMetadata> UpdateAsync(object id, Stream stream);

        /// <summary>
        /// Updates file metadata and file content.
        /// </summary>
        /// <param name="metadata">Metadata of the file to be updated.</param>
        /// <param name="stream">Stream of the file.</param>
        /// <returns>Task of file metadata.</returns>
        Task<IFileMetadata> UpdateAsync(IFileMetadata metadata, Stream stream);
    }
}
