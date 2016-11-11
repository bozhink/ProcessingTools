namespace ProcessingTools.Contracts.Files.Generators
{
    using System.Threading.Tasks;

    public interface IFileNameGenerator
    {
        Task<string> Generate(string baseFileFullName, int maximalFileNameLength, bool returnFullName = true);

        Task<string> Generate(string directoryPath, string baseFileName, int maximalFileNameLength, bool returnFullName = false);
    }
}
