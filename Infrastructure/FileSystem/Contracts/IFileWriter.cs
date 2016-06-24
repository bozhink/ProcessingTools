namespace ProcessingTools.FileSystem.Contracts
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IFileWriter
    {
        /// <summary>
        /// Writes a stream to a file.
        /// </summary>
        /// <param name="stream">Stream object to be written to fileName.</param>
        /// <param name="fileName">Name of the output file. Should be full-path name or relative.</param>
        /// <param name="basePath">Path to directory in which fileName should be.</param>
        /// <returns>Content length of the written file.</returns>
        Task<long> Write(Stream stream, string fileName, string basePath = null);

        /// <summary>
        /// Deletes file.
        /// </summary>
        /// <param name="fileName">Name of the output file. Should be full-path name or relative.</param>
        /// <param name="basePath">Path to directory in which fileName should be.</param>
        /// <returns>Awaitable task.</returns>
        Task Delete(string fileName, string basePath = null);

        /// <summary>
        /// Creates new unique file name in given full path.
        /// </summary>
        /// <param name="fileName">Name of the output file. Should be full-path name or relative.</param>
        /// <param name="basePath">Path to directory in which fileName should be.</param>
        /// <param name="length">Length of the file name part of the new file path.</param>
        /// <returns>Unique file path based on the <paramref name="path"/>.</returns>
        Task<string> GetNewFilePath(string fileName, string basePath, int length);
    }
}
