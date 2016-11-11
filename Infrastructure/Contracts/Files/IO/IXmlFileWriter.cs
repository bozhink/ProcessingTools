namespace ProcessingTools.Contracts.Files.IO
{
    using System.Threading.Tasks;
    using System.Xml;

    public interface IXmlFileWriter
    {
        XmlWriterSettings WriterSettings { get; set; }

        Task<object> Write(string fullName, XmlReader reader);
    }
}
