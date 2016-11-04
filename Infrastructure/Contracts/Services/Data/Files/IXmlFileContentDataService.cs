namespace ProcessingTools.Contracts.Services.Data.Files
{
    using System.Threading.Tasks;
    using System.Xml;

    public interface IXmlFileContentDataService
    {
        XmlReaderSettings ReaderSettings { get; set; }

        XmlWriterSettings WriterSettings { get; set; }

        XmlReader GetReader(object id);

        Task<object> Write(object id, XmlWriter writer);
    }
}
