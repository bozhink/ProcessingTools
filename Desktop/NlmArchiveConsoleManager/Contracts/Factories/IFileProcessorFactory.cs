namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories
{
    using Contracts.Core;
    using Contracts.Models;

    public interface IFileProcessorFactory
    {
        IFileProcessor CreateFileProcessor(string fileName, IJournal journal);
    }
}
