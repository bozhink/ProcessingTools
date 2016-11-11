namespace ProcessingTools.Contracts.Files.IO
{
    using System.Threading.Tasks;

    public interface ITextFileReader : IFileReader
    {
        Task<string> ReadAllText(string fullName);
    }
}
