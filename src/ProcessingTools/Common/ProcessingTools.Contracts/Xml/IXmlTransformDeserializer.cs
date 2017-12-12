namespace ProcessingTools.Contracts.Xml
{
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Processors;

    public interface IXmlTransformDeserializer
    {
        Task<T> Deserialize<T>(IXmlTransformer transformer, string xml);

        Task<T> Deserialize<T>(IXmlTransformer transformer, XmlNode node);
    }
}
