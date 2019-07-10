// <copyright file="IFilesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.IO;
using System.Threading.Tasks;
using ProcessingTools.Contracts.Models.Files;

namespace ProcessingTools.Contracts.Services.Files
{
    /// <summary>
    /// Files data service.
    /// </summary>
    public interface IFilesDataService
    {
        /// <summary>
        /// Creates a file by file meta-data and stream.
        /// </summary>
        /// <param name="metadata">Meta-data of the file.</param>
        /// <param name="stream">Stream of the content of the file.</param>
        /// <returns>Task of the file meta-data.</returns>
        Task<IFileMetadata> CreateAsync(IFileMetadata metadata, Stream stream);

        /// <summary>
        /// Deletes a file by ID.
        /// </summary>
        /// <param name="id">ID of the file to be deleted.</param>
        /// <returns>Task of deletion status.</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Gets meta-data of a file by ID.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <returns>Task of file meta-data.</returns>
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
        /// Updates file meta-data.
        /// </summary>
        /// <param name="metadata">Meta-data of the file to be updated.</param>
        /// <returns>Task of file meta-data.</returns>
        Task<IFileMetadata> UpdateAsync(IFileMetadata metadata);

        /// <summary>
        /// Updates file content by ID.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <param name="stream">Stream of the file.</param>
        /// <returns>Task of file meta-data.</returns>
        Task<IFileMetadata> UpdateAsync(object id, Stream stream);

        /// <summary>
        /// Updates file meta-data and file content.
        /// </summary>
        /// <param name="metadata">Meta-data of the file to be updated.</param>
        /// <param name="stream">Stream of the file.</param>
        /// <returns>Task of file meta-data.</returns>
        Task<IFileMetadata> UpdateAsync(IFileMetadata metadata, Stream stream);

        /// <summary>
        /// Writes stream to a file.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <param name="stream"><see cref="Stream"/> of the file content.</param>
        /// <returns>Task.</returns>
        Task<object> WriteAsync(object id, Stream stream);

        /// <summary>
        /// Writes content of a <see cref="StreamReader"/> to a file.
        /// </summary>
        /// <param name="id">ID of the file.</param>
        /// <param name="streamReader"><see cref="StreamReader"/> for the file content.</param>
        /// <returns>Task.</returns>
        Task<object> WriteAsync(object id, StreamReader streamReader);
    }
}
