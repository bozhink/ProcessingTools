namespace ProcessingTools.Serialization.Contracts
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IXmlDeserializer<T>
    {
        Task<T> Deserialize(Stream stream);
    }
}
