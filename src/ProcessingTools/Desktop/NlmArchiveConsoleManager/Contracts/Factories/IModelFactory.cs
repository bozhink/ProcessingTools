namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories
{
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Models;

    public interface IModelFactory
    {
        IFileReplacementModel CreateFileReplacementModel(string destination, string originalFileName, string source);
    }
}
