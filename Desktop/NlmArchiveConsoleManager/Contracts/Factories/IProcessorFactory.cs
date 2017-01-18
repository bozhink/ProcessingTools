namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories
{
    using Contracts.Core;
    using ProcessingTools.Services.Data.Contracts.Models.Meta;

    public interface IProcessorFactory
    {
        IDirectoryProcessor CreateDirectoryProcessor(string direcoryName, IJournal journal);

        IFileProcessor CreateFileProcessor(string fileName, IJournal journal);
    }
}
