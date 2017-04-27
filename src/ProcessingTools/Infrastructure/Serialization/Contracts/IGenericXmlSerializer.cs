namespace ProcessingTools.Serialization.Contracts
{
    using System.Threading.Tasks;
    using System.Xml;

    public interface IXmlSerializer<T>
    {
        Task<XmlNode> Serialize(T @object);

        void SetNamespaces(XmlNamespaceManager namespaceManager);
    }
}
