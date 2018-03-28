// <copyright file="IFilesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Documents
{
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Models.Contracts.Documents.Files;
    using ProcessingTools.Models.Contracts.Documents.Files;

    /// <summary>
    /// Files data access object.
    /// </summary>
    public interface IFilesDataAccessObject : IDataAccessObject<IFileDataModel, IFileDetailsDataModel, IFileInsertModel, IFileUpdateModel>
    {
        /// <summary>
        /// Sets file content for text file.
        /// </summary>
        /// <param name="id">Object ID of the file to be updated.</param>
        /// <param name="content">Content as string to be set.</param>
        /// <returns>Resultant file content length.</returns>
        Task<long> SetFileContentAsync(object id, string content);

        /// <summary>
        /// Sets file content for binary file.
        /// </summary>
        /// <param name="id">Object ID of the file to be updated.</param>
        /// <param name="content">Content as byte array to be set.</param>
        /// <returns>Resultant file content length.</returns>
        Task<long> SetFileContentAsync(object id, byte[] content);

        /// <summary>
        /// Sets file content.
        /// </summary>
        /// <param name="id">Object ID of the file to be updated.</param>
        /// <param name="content">Content as stream to be set.</param>
        /// <returns>Resultant file content length.</returns>
        Task<long> SetFileContentAsync(object id, Stream content);

        /// <summary>
        /// Gets file content as string.
        /// </summary>
        /// <param name="id">Object ID of the file.</param>
        /// <returns>Content of the file as string.</returns>
        Task<string> GetFileContentAsStringAsync(object id);

        /// <summary>
        /// Gets file content as byte array.
        /// </summary>
        /// <param name="id">Object ID of the file.</param>
        /// <returns>Content of the file as byte array.</returns>
        Task<byte[]> GetFileContentAsByteArrayAsync(object id);

        /// <summary>
        /// Gets file content as stream.
        /// </summary>
        /// <param name="id">Object ID of the file.</param>
        /// <returns>Content of the file as stream.</returns>
        Task<Stream> GetFileContentAsStreamAsync(object id);
    }
}
