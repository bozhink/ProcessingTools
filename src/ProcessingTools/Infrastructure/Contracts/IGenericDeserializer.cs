namespace ProcessingTools.Contracts
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IGenericDeserializer<T>
    {
        Task<T> DeserializeAsync(Stream stream);
    }
}
