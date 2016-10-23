namespace ProcessingTools.Xml.Contracts.Serialization
{
    using System.Threading.Tasks;
    using Transformers;

    public interface IXmlTransformDeserializer<TTransformer, TResult>
        where TTransformer : IXmlTransformer
    {
        Task<TResult> Deserialize(string xml);
    }
}
