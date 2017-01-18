namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories
{
    using Models;

    public interface IModelFactory
    {
        IFileReplacementModel CreateFileReplacementModel(string destination, string originalFileName, string source);
    }
}
