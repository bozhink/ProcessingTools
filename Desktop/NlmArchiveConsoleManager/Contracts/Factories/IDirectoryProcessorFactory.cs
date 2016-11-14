namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories
{
    using Contracts.Core;
    using Contracts.Models;

    public interface IDirectoryProcessorFactory
    {
        IDirectoryProcessor CreateDirectoryProcessor(string direcoryName, IJournal journal);
    }
}
