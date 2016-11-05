namespace ProcessingTools.Contracts.IO
{
    using System.Threading.Tasks;
    using System.Xml;

    public interface IXmlFileContentWriter
    {
        XmlWriterSettings WriterSettings { get; set; }

        Task<object> Write(object id, XmlReader reader);
    }
}
