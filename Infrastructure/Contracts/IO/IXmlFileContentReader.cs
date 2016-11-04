namespace ProcessingTools.Contracts.IO
{
    using System.Xml;

    public interface IXmlFileContentReader
    {
        XmlReaderSettings ReaderSettings { get; set; }

        XmlReader GetReader(object id);
    }
}
