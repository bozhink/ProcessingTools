namespace ProcessingTools.FileSystem.Contracts
{
    using System.IO;

    public interface IFileReader
    {
        /// <summary>
        /// Reads a file as stream.
        /// </summary>
        /// <param name="fileName">Name of the input file. Should be full-path name or relative.</param>
        /// <param name="basePath">Path to directory in which fileName should be.</param>
        /// <returns>Stream object for fileName.</returns>
        Stream ReadToStream(string fileName, string basePath = null);

        /// <summary>
        /// Reads a file to a StreamReader.
        /// </summary>
        /// <param name="fileName">Name of the input file. Should be full-path name or relative.</param>
        /// <param name="basePath">Stream object for fileName.</param>
        /// <returns>StreamReader object for fileName.</returns>
        StreamReader GetReader(string fileName, string basePath = null);
    }
}
