namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories
{
    using ProcessingTools.Models.Contracts.Documents;
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Core;

    public interface IProcessorFactory
    {
        IDirectoryProcessor CreateDirectoryProcessor(string direcoryName, IJournalMeta journalMeta);

        IFileProcessor CreateFileProcessor(string fileName, IJournalMeta journalMeta);
    }
}
