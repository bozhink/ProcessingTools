namespace ProcessingTools.Contracts.Files.IO
{
    using System.Threading.Tasks;

    public interface ITextFileWriter : IFileWriter
    {
        Task<object> Write(string fullName, string text);
    }
}
