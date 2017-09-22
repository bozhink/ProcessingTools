// <copyright file="IXmlFileReaderWriter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.IO
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;

    /// <summary>
    /// Reader and writer for XML files.
    /// </summary>
    public interface IXmlFileReaderWriter
    {
        /// <summary>
        /// Gets or sets the settings of the reader.
        /// </summary>
        XmlReaderSettings ReaderSettings { get; set; }

        /// <summary>
        /// Gets or sets the settings of the writer.
        /// </summary>
        XmlWriterSettings WriterSettings { get; set; }

        /// <summary>
        /// Deletes file.
        /// </summary>
        /// <param name="fileName">Name of the output file. Should be full-path name or relative.</param>
        /// <param name="basePath">Path to directory in which fileName should be.</param>
        /// <returns>Task</returns>
        Task DeleteAsync(string fileName, string basePath);

        /// <summary>
        /// Creates new unique file name in given full path.
        /// </summary>
        /// <param name="fileName">Name of the output file. Should be full-path name or relative.</param>
        /// <param name="basePath">Path to directory in which fileName should be.</param>
        /// <param name="length">Length of the file name part of the new file path.</param>
        /// <returns>Unique file path based on the path.</returns>
        Task<string> GetNewFilePathAsync(string fileName, string basePath, int length);

        /// <summary>
        /// Reads a file to a XmlReader.
        /// </summary>
        /// <param name="fileName">Name of the input file. Should be full-path name or relative.</param>
        /// <param name="basePath">Stream object for fileName.</param>
        /// <returns>XmlReader object for fileName.</returns>
        XmlReader GetXmlReader(string fileName, string basePath);

        /// <summary>
        /// Reads a file as stream.
        /// </summary>
        /// <param name="fileName">Name of the input file. Should be full-path name or relative.</param>
        /// <param name="basePath">Path to directory in which fileName should be.</param>
        /// <returns>Stream object for fileName.</returns>
        Stream ReadToStream(string fileName, string basePath);

        /// <summary>
        /// Writes a stream to a file.
        /// </summary>
        /// <param name="stream">Stream object to be written to fileName.</param>
        /// <param name="fileName">Name of the output file. Should be full-path name or relative.</param>
        /// <param name="basePath">Path to directory in which fileName should be.</param>
        /// <returns>Content length of the written file.</returns>
        Task<long> WriteAsync(Stream stream, string fileName, string basePath);
    }
}
