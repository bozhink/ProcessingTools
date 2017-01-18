namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories
{
    using Contracts.Core;
    using ProcessingTools.Services.Data.Contracts.Models.Meta;

    public interface IDirectoryProcessorFactory
    {
        IDirectoryProcessor CreateDirectoryProcessor(string direcoryName, IJournal journal);
    }
}
