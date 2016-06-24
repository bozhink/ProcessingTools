namespace ProcessingTools.FileSystem.Contracts
{
    using System.Xml;

    public interface IXmlFileReaderWriter : IFileReader, IFileWriter
    {
        XmlReaderSettings ReaderSettings { get; set; }

        XmlWriterSettings WriterSettings { get; set; }

        /// <summary>
        /// Reads a file to a XmlReader.
        /// </summary>
        /// <param name="fileName">Name of the input file. Should be full-path name or relative.</param>
        /// <param name="basePath">Stream object for fileName.</param>
        /// <returns>XmlReader object for fileName.</returns>
        XmlReader GetXmlReader(string fileName, string basePath = null);
    }
}
