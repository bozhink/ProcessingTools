namespace ProcessingTools.Contracts
{
    using System.Xml;

    public interface IXmlNamespaceManagerProvider
    {
        XmlNamespaceManager Create();
    }
}
