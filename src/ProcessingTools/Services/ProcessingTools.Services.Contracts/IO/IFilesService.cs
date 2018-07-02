// <copyright file="IFilesService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.IO
{
    using System.IO;

    /// <summary>
    /// Files Service.
    /// </summary>
    public interface IFilesService
    {
        /// <summary>
        /// Returns the full name of the files' directory.
        /// </summary>
        /// <returns>The full name of the files' directory.</returns>
        string GetFilesDirectoryFullName();

        /// <summary>
        /// Returns the full name of the temporary directory.
        /// </summary>
        /// <returns>The full name of the temporary directory.</returns>
        string GetTempDirectoryFullName();

        /// <summary>
        /// Resolves a file name and returns its full name.
        /// </summary>
        /// <param name="fileName">File name to be resolved.</param>
        /// <returns>Full name of the file.</returns>
        string GetFileFullName(string fileName);

        /// <summary>
        /// Returns a new local file name with default settings.
        /// </summary>
        /// <returns>New local file name with default settings.</returns>
        string GetNewFileName();

        /// <summary>
        /// Returns a new local file name with specified maximal length.
        /// </summary>
        /// <param name="maximalLength">Maximal length of the generated file name.</param>
        /// <returns>New local file name with specified maximal length.</returns>
        string GetNewFileName(int maximalLength);

        /// <summary>
        /// Returns a new local file name based on a specified file name.
        /// </summary>
        /// <param name="fileName">Base file name of the new file name.</param>
        /// <returns>New local file name based on a specified file name.</returns>
        string GetNewFileName(string fileName);

        /// <summary>
        /// Returns a new local file name based on a specified file name with specified maximal length.
        /// </summary>
        /// <param name="fileName">Base file name of the new file name.</param>
        /// <param name="maximalLength">Maximal length of the generated file name.</param>
        /// <returns>New local file name based on a specified file name with specified maximal length.</returns>
        string GetNewFileName(string fileName, int maximalLength);

        /// <summary>
        /// Copies one file to another name with default overwrite behavior.
        /// </summary>
        /// <param name="sourceFileName">Source file name.</param>
        /// <param name="destinationFileName">Destination file name.</param>
        void CopyFile(string sourceFileName, string destinationFileName);

        /// <summary>
        /// Copies one file to another name with specified overwrite behavior.
        /// </summary>
        /// <param name="sourceFileName">Source file name.</param>
        /// <param name="destinationFileName">Destination file name.</param>
        /// <param name="overwrite">Specified overwrite behavior.</param>
        void CopyFile(string sourceFileName, string destinationFileName, bool overwrite);

        /// <summary>
        /// Copies a file to new file with name generated with default parameters.
        /// </summary>
        /// <param name="sourceFileName">Source file name.</param>
        /// <returns>Name of the output file.</returns>
        string CopyToNewFile(string sourceFileName);

        /// <summary>
        /// Copies a <see cref="Stream"/> to new file with name generated with default parameters.
        /// </summary>
        /// <param name="sourceStream">Source <see cref="Stream"/>.</param>
        /// <returns>Name of the output file.</returns>
        string CopyToNewFile(Stream sourceStream);

        /// <summary>
        /// Copies a file to new file with name generated with specified maximal file name length.
        /// </summary>
        /// <param name="sourceFileName">Source file name.</param>
        /// <param name="maximalFileNameLength">Maximal length of the output file name.</param>
        /// <returns>Name of the output file.</returns>
        string CopyToNewFile(string sourceFileName, int maximalFileNameLength);

        /// <summary>
        /// Copies a <see cref="Stream"/> to new file with name generated with specified maximal file name length.
        /// </summary>
        /// <param name="sourceStream">Source <see cref="Stream"/>.</param>
        /// <param name="maximalFileNameLength">Maximal length of the output file name.</param>
        /// <returns>Name of the output file.</returns>
        string CopyToNewFile(Stream sourceStream, int maximalFileNameLength);

        /// <summary>
        /// Copies a file to new file with name generated with local file name based on a specified file name.
        /// </summary>
        /// <param name="sourceFileName">Source file name.</param>
        /// <param name="baseFileName">Base file name of the new file name.</param>
        /// <returns>Name of the output file.</returns>
        string CopyToNewFile(string sourceFileName, string baseFileName);

        /// <summary>
        /// Copies a <see cref="Stream"/> to new file with name generated with local file name based on a specified file name.
        /// </summary>
        /// <param name="sourceStream">Source <see cref="Stream"/>.</param>
        /// <param name="baseFileName">Base file name of the new file name.</param>
        /// <returns>Name of the output file.</returns>
        string CopyToNewFile(Stream sourceStream, string baseFileName);

        /// <summary>
        /// Copies a file to new file with name generated with local file name based on a specified file name with specified maximal length.
        /// </summary>
        /// <param name="sourceFileName">Source file name.</param>
        /// <param name="baseFileName">Base file name of the new file name.</param>
        /// <param name="maximalFileNameLength">Maximal length of the output file name.</param>
        /// <returns>Name of the output file.</returns>
        string CopyToNewFile(string sourceFileName, string baseFileName, int maximalFileNameLength);

        /// <summary>
        /// Copies a <see cref="Stream"/> to new file with name generated with local file name based on a specified file name with specified maximal length.
        /// </summary>
        /// <param name="sourceStream">Source <see cref="Stream"/>.</param>
        /// <param name="baseFileName">Base file name of the new file name.</param>
        /// <param name="maximalFileNameLength">Maximal length of the output file name.</param>
        /// <returns>Name of the output file.</returns>
        string CopyToNewFile(Stream sourceStream, string baseFileName, int maximalFileNameLength);
    }
}
