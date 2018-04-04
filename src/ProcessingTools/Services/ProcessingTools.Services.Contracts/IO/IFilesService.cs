// <copyright file="IFilesService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.IO
{
    /// <summary>
    /// Files service.
    /// </summary>
    public interface IFilesService
    {
        /// <summary>
        /// Resolves full file name.
        /// </summary>
        /// <param name="fileName">Name of file to be resolved.</param>
        /// <returns>Full file name.</returns>
        string GetFullFileName(string fileName);

        /// <summary>
        /// Resolves full file name of the original file.
        /// </summary>
        /// <param name="fileName">Name of file to be resolved.</param>
        /// <returns>Full file name.</returns>
        string GetOriginalFullFileName(string fileName);

        /// <summary>
        /// Checks whether file exists.
        /// </summary>
        /// <param name="fileName">Name of file to be resolved.</param>
        /// <returns>True if file exists.</returns>
        bool FileExists(string fileName);

        /// <summary>
        /// Generates new file name.
        /// </summary>
        /// <param name="fileName">Old file name.</param>
        /// <param name="fileNameLength">Required maximal length if the new file.</param>
        /// <returns>New file name.</returns>
        string GenerateNewFileName(string fileName, int fileNameLength);

        /// <summary>
        /// Moves a file to the data directory.
        /// </summary>
        /// <param name="fileName">Name of the file to be moved.</param>
        /// <param name="fileNameLength">Required maximal length if the new file.</param>
        /// <returns>The new name of the moved file.</returns>
        string MoveFile(string fileName, int fileNameLength);

        /// <summary>
        /// Copies a file from the data directory to a specified destination.
        /// </summary>
        /// <param name="fileName">Name of the file to be copied.</param>
        /// <param name="destination">Destination path for the file.</param>
        void CopyOriginalFile(string fileName, string destination);
    }
}
