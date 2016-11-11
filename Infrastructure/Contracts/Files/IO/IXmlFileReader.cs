namespace ProcessingTools.Contracts.Files.IO
{
    using System.Xml;

    public interface IXmlFileReader
    {
        XmlReaderSettings ReaderSettings { get; set; }

        XmlReader GetReader(string fullName);
    }
}
