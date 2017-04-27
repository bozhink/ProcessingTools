namespace ProcessingTools.Contracts.Files.IO
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IFileWriter
    {
        Task<object> Write(string fullName, Stream stream);

        Task<object> Write(string fullName, StreamReader streamReader);
    }
}
