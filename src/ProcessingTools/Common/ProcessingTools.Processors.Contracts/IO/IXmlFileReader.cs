namespace ProcessingTools.Contracts.Files.IO
{
    using System.Threading.Tasks;
    using System.Xml;

    public interface IXmlFileReader
    {
        XmlReaderSettings ReaderSettings { get; set; }

        XmlReader GetReader(string fullName);

        Task<XmlDocument> ReadXml(string fullName);
    }
}
