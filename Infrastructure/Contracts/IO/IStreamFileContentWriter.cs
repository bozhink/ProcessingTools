namespace ProcessingTools.Contracts.IO
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IStreamFileContentWriter
    {
        Task<object> Write(object id, Stream stream);

        Task<object> Write(object id, StreamWriter streamWriter);
    }
}
