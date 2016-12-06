namespace ProcessingTools.Xml.Contracts.Serialization
{
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts;

    public interface IXmlTransformDeserializer<TTransformer>
        where TTransformer : IXmlTransformer
    {
        Task<T> Deserialize<T>(string xml);

        Task<T> Deserialize<T>(XmlNode node);
    }
}
