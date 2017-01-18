namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories
{
    using Contracts.Core;
    using ProcessingTools.Services.Data.Contracts.Models.Meta;

    public interface IFileProcessorFactory
    {
        IFileProcessor CreateFileProcessor(string fileName, IJournal journal);
    }
}
