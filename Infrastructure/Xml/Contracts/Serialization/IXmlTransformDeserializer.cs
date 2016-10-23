namespace ProcessingTools.Xml.Contracts.Serialization
{
    using System.Threading.Tasks;
    using Transformers;

    public interface IXmlTransformDeserializer<TTransformer>
        where TTransformer : IXmlTransformer
    {
        Task<T> Deserialize<T>(string xml);
    }
}
