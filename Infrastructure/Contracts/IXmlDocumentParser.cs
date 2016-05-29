namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;
    using System.Xml;

    public interface IXmlDocumentParser
    {
        Task Parse(XmlDocument document, XmlNamespaceManager namespaceManager);
    }
}