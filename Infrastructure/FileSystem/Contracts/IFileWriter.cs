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
        /// <param name="fileName">Name of the file. Should be full-path name or relative.</param>
        /// <param name="basePath">Path to directory in which fileName should be.</param>
        /// <returns>Awaitable task.</returns>
        Task Write(Stream stream, string fileName, string basePath = null);
    }
}
