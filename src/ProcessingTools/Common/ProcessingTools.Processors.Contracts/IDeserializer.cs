namespace ProcessingTools.Contracts
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IDeserializer
    {
        Task<T> DeserializeAsync<T>(Stream stream);
    }
}
