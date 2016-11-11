namespace ProcessingTools.Contracts.Files.IO
{
    using System.Threading.Tasks;

    public interface ITextFileReader
    {
        Task<string> ReadAllText(string fullName);
    }
}
