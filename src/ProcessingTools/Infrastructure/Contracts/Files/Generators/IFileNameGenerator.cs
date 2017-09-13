namespace ProcessingTools.Contracts.Files.Generators
{
    using System.Threading.Tasks;

    public interface IFileNameGenerator
    {
        Task<string> GenerateAsync(string baseFileFullName, int maximalFileNameLength, bool returnFullName = true);

        Task<string> GenerateAsync(string directoryPath, string baseFileName, int maximalFileNameLength, bool returnFullName = false);
    }
}
