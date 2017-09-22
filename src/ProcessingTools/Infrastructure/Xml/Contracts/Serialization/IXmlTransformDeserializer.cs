namespace ProcessingTools.Xml.Contracts.Serialization
{
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Processors.Contracts;

    public interface IXmlTransformDeserializer
    {
        Task<T> Deserialize<T>(IXmlTransformer transformer, string xml);

        Task<T> Deserialize<T>(IXmlTransformer transformer, XmlNode node);
    }
}
